using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Query.Nats.Config
{
    public class NatsQuerySlaveModuleBuilder : CoreModuleBuilder
    {
        public NatsQuerySlaveModuleBuilder(IServiceCollection services) : base(services)
        {
        }

        public override IServiceCollection AddServices()
        {
            return Collection
                .AddNatsQuerySlave();
        }
    }
}