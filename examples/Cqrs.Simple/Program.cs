using System;
using System.Linq;
using Ef.Dal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Cqrs.Simple
{
    /// <summary>
    /// Endpoint
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method, app starter
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                var host = CreateHostBuilder(args)
                    .Build();

                if (args.Any(arg => arg.Equals("--migrate-db")))
                {
                    using var scope = host.Services.CreateScope();
                    var services = scope.ServiceProvider;
                    SeedData.Initialize(services);
                    Console.WriteLine("EF MIGRATION SOCCEED");
                }
                else
                {
                    host.Run();
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseDefaultServiceProvider(options => options.ValidateScopes = false);
    }
}