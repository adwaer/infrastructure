using In.Common.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace In.Common.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            return services.AddSingleton<IDiScope, ServiceLocator>()
                .AddSingleton<ITypeFactory, TypeFactory>();
        }
    }
}