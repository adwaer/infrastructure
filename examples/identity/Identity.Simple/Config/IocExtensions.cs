using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Identity.CommandHandlers;
using Identity.Dal;
using Identity.Domain.Models;
using Identity.QueryHandlers;
using In.Auth.Config;
using In.Auth.Identity.Server.Config;
using In.Common.Config;
using In.Cqrs.Command.Simple.Config;
using In.Cqrs.Query.Simple.Config;
using In.DataAccess.EfCore.Config;
using In.DataMapping.Automapper.Config;
using In.Logging.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
                    Version = "v1",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Azat Buriev",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/azat-buriev-b83b3a5a/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
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
                    , optionsBuilder => optionsBuilder.UseInMemoryDatabase("indetity.simple")
                    , (
                        null, null
                        , new PasswordOptions()
                        {
                            RequireDigit = false, RequireLowercase = false, RequireUppercase = false,
                            RequiredUniqueChars = 0, RequireNonAlphanumeric = false
                        }
                        , null, null, null, null))
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
                .Configure<AuthenticationSettings>(configuration.GetSection("AuthenticationOptions"));
        }

        /// <summary>
        /// settings options
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            var section = configuration.GetSection("AuthenticationOptions");
            var options = section.Get<AuthenticationSettings>();

            return services
                .AddAuth(options);
        }
    }
}