using In.AppBuilder;
using In.Cqrs.Nats.Config;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Query.Nats.Config
{
    public class NatsQuerySlaveModuleBuilder : CoreModuleBuilder
    {
        private readonly NatsSenderOptions _natsSettings;

        public NatsQuerySlaveModuleBuilder(IServiceCollection services, NatsSenderOptions natsSettings) : base(services)
        {
            _natsSettings = natsSettings;
        }

        public override IServiceCollection AddServices()
        {
            return Collection
                .AddNatsServices(_natsSettings)
                .AddNatsQuerySlave();
        }
    }
}