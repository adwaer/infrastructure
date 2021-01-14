using System.Text;
using In.Auth.Services;
using In.Auth.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace In.Auth.Config
{
    public static class IocExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services,
            AuthenticationSettings settings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = settings.Url,
                        ValidateAudience = true,
                        ValidAudience = settings.Url,
                        ValidateLifetime = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.SecretJwtKey)),
                    };
                });

            return services
                .AddTransient<IUserContextService, UserContextService>();
        }
    }
}