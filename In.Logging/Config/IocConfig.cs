﻿using In.Logging.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace In.Logging.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddLoggerServices(this IServiceCollection services)
        {
            return services
                .AddTransient<ILogService, LogService>();
        }
    }
}