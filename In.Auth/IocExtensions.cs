using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace In.Auth
{
    public static class IocExtensions
    {
        public static IServiceCollection AddAuthOptions(this IServiceCollection services,
            IConfiguration configuration, string sectionName = "AuthenticationOptions")
        {
            return services
                .Configure<AuthenticationSettings>(configuration.GetSection(sectionName));
        }

        public static IServiceCollection AddAuth(this IServiceCollection services,
            IConfiguration configuration, string sectionName = "AuthenticationOptions")
        {
            var section = configuration.GetSection(sectionName);
            var authenticationOptions = section.Get<AuthenticationSettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authenticationOptions.Url,
                        ValidateAudience = true,
                        ValidAudience = authenticationOptions.Url,
                        ValidateLifetime = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationOptions.SecretJwtKey)),
                    };
                });

            return services;
        }
    }
}