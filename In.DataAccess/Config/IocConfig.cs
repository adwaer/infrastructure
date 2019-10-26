using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace In.DataAccess.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddQueries(this IServiceCollection services, Assembly[] assemblies)
        {
            return services;
        }
    }
}