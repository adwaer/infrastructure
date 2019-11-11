using In.Logs.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace In.Logs.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            return services
                .AddScoped<ILogService, LogService>();
        }
    }
}