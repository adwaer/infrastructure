using System;
using System.Collections.Generic;
using System.IO;
using Identity.CommandHandlers;
using Identity.Dal;
using Identity.Domain.Models;
using Identity.QueryHandlers;
using In.Auth.Identity.Server.Config;
using In.Common;
using In.Common.Config;
using In.Cqrs.Command;
using In.Cqrs.Command.Simple.Config;
using In.Cqrs.Query.Simple.Config;
using In.DataAccess.EfCore.Config;
using In.DataMapping.Automapper.Config;
using In.Logging.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Web.Infrastructure;

namespace Identity.Simple.Config
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

                var scheme = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                };
                var security = new OpenApiSecurityRequirement
                {
                    {scheme, new List<string>()}
                };
                
                c.AddSecurityDefinition("Bearer", scheme);
                c.AddSecurityRequirement(security);

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Identity.Simple.xml"));
            });
        }

        /// <summary>
        /// Identity serv
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentitySrv(this IServiceCollection services)
        {
            return new IdentityServerModuleBuilder<User, IdentityCtx>(services
                    , optionsBuilder => optionsBuilder.UseInMemoryDatabase("indetity.simple"))
                .AddServices()
                .AddEfCoreServices<IdentityCtx>(typeof(IdentityCtx).Assembly);
        }

        /// <summary>
        /// Add command sender and handlers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCqrsSimpleCommands(this IServiceCollection services)
        {
            return new SimpleCommandModuleBuilder<SimpleMessageResult>(services, typeof(CommandHandlersConfig).Assembly)
                .AddServices();
        }

        /// <summary>
        /// Add query sender and handlers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCqrsSimpleQueries(this IServiceCollection services)
        {
            return new SimpleQueryModuleBuilder(services, typeof(QueryHandlersConfig).Assembly)
                .AddServices();
        }

        /// <summary>
        /// settings options
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfigOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .Configure<AuthenticationOptions>(configuration.GetSection("AuthenticationOptions"));
        }
    }
}