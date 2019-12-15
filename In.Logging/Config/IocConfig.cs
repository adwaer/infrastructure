using In.Logging.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace In.Logging.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            return services
                .AddSingleton<ILogService, LogService>();
        }
    }
}