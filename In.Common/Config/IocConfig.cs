using In.Common.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace In.Common.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            return services.AddScoped<IDiScope, ServiceLocator>()
                .AddSingleton<ITypeFactory, TypeFactory>();
        }
    }
}