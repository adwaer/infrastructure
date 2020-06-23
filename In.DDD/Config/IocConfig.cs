using System.Reflection;
using In.Common;
using In.DDD.Events;
using In.DDD.Repository;
using In.DDD.Repository.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace In.DDD.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddDddServices(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            return services.AddScoped<SimpleDomainEventRepository>(cf =>
                    {
                        var service = (IDiScope) cf.GetService(typeof(IDiScope));
                        DomainEvents.Init(service);

                        return null;
                    })
                    .RegisterAssemblyImplementationsScoped(typeof(IEventMsgHandle<>), assemblies)
                    .RegisterAssemblyImplementationsScoped(typeof(IDomainEventRepository), assemblies);
        }
    }
}