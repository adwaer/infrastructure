using In.AppBuilder;
using In.Cqrs.Nats.Config;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Command.Nats.Config
{
    public class NatsCommandSlaveModuleBuilder : CoreModuleBuilder
    {
        private readonly NatsSenderOptions _natsSettings;

        public NatsCommandSlaveModuleBuilder(IServiceCollection services, NatsSenderOptions natsSettings) :
            base(services)
        {
            _natsSettings = natsSettings;
        }

        public override IServiceCollection AddServices()
        {
            return Collection
                .AddNatsServices(_natsSettings)
                .AddNatsCommandSlave();
        }
    }
}