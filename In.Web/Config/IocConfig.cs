using In.Web.Implementations;
using In.Web.Middlerwares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Web.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            return services.AddScoped<IUserContextService, UserContextService>();
        }

        public static IApplicationBuilder UseErrorsMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}