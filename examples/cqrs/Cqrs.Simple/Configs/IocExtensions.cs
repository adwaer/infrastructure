using System;
using System.IO;
using Cqrs.CommandHandlers.Features.Ordering;
using Cqrs.Domain.Models;
using Cqrs.EventHandlers.Features.Ordering;
using Cqrs.QueryHandlers.Features.Ordering;
using Ef.Dal;
using In.Common.Config;
using In.Cqrs.Command.Simple.Config;
using In.Cqrs.Query.Simple.Config;
using In.DataAccess.EfCore.Config;
using In.DataMapping.Automapper.Config;
using In.DDD.Config;
using In.Logging.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Cqrs.Simple.Configs
{
    /// <summary>
    /// Config extensions
    /// </summary>
    public static class IocExtensions
    {
        /// <summary>
        /// Adds common services
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            var builder = new CommonModuleBuilder(services);
            return builder.AddServices();
        }
        
        /// <summary>
        /// Add logging services
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddLogs(this IServiceCollection services)
        {
            var builder = new LoggingModuleBuilder(services);
            return builder.AddServices();
        }

        /// <summary>
        /// Add command sender and handlers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCqrsSimpleCommands(this IServiceCollection services)
        {
            var builder = new SimpleCommandModuleBuilder<SimpleMessageResult>(services, typeof(ChangeBalanceHandler).Assembly);
            return builder.AddServices();
        }

        /// <summary>
        /// Add query sender and handlers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCqrsSimpleQueries(this IServiceCollection services)
        {
            var builder = new SimpleQueryModuleBuilder(services, typeof(UserBalanceQueryHandler).Assembly);
            return builder.AddServices();
        }
        
        /// <summary>
        /// Add DDD
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDdd(this IServiceCollection services)
        {
            var builder = new DddModuleBuilder(services, typeof(WriteConsoleAfterUserChangeEventHandler).Assembly);
            return builder.AddServices();
        }
        
        /// <summary>
        /// Add ef core providers
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddEfDb(this IServiceCollection services, IConfiguration configuration)
        {
            const string connName = "DefaultConnection";
            var conn = configuration.GetConnectionString(connName);
            
            var builder = new DataAccessEfCoreModuleBuilder<EfCtx>(services, typeof(EfCtx).Assembly);
            return builder
                .AddServices()
                // .AddDbContext<EfCtx>(x => x.UseInMemoryDatabase("cqrs.simple"));
                .AddDbContext<EfCtx>(x => x.UseNpgsql(conn));
        }
        
        /// <summary>
        /// Add automapper mappings
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddDMAutomapper(this IServiceCollection services)
        {
            var builder = new DataMappingAutomapperModuleBuilder(services);
            return builder.AddServices();
        }
        
        
        /// <summary>
        /// cons for CORS policy
        /// </summary>
        public const string CorsPolicy = "CorsPolicy";
        
        /// <summary>
        /// Cors policy
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
        {
            var domains = config["Cors:Domains"].Split(';', ',');
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder => builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(domains)
                        .AllowCredentials());
            });

            return services;
        }
        
        /// <summary>
        /// Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Simple API",
                    Version = "v1"
                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Cqrs.Simple.xml"));

            });
        }
    }
}
