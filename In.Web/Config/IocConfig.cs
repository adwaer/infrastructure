using In.Web.Middlerwares;
using Microsoft.AspNetCore.Builder;

namespace In.Web.Config
{
    public static class IocConfig
    {
        public static IApplicationBuilder UseErrorsMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}