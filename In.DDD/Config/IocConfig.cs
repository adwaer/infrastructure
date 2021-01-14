using System.Reflection;
using In.Common;
using In.DDD.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace In.DDD.Config
{
    public static class IocConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies">
        /// for: IDomainUow / IDomainRepository / IDomainMessageDispatcher / IDomainMessageHandler
        /// note: only IDomainMessageHandler don't have simple implementation, other 3 have
        /// </param>
        /// <returns></returns>
        public static IServiceCollection AddDddServices(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            return services
                .AddTransient(typeof(IDomainUow<,>), typeof(SimpleDomainUow<,>))
                .RegisterAssemblyImplementationsTransient(typeof(IDomainUow<,>), assemblies)
                .AddScoped(typeof(IDomainRepository<,>), typeof(SimpleDomainRepository<,>))
                .RegisterAssemblyImplementationsScoped(typeof(IDomainRepository<,>), assemblies)
                .AddTransient(typeof(IDomainMessageDispatcher<>), typeof(SimpleDomainMessageDispatcher<>))
                .RegisterAssemblyImplementationsTransient(typeof(IDomainMessageDispatcher<>), assemblies)
                .RegisterAssemblyImplementationsTransient(typeof(IDomainMessageHandler<>), assemblies);
        }
    }
}