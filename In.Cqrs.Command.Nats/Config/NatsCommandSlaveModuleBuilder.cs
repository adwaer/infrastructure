using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Command.Nats.Config
{
    public class NatsCommandSlaveModuleBuilder : CoreModuleBuilder
    {
        public NatsCommandSlaveModuleBuilder(IServiceCollection services)
            : base(services)
        {
        }

        public override IServiceCollection AddServices()
        {
            return Collection
                .AddNatsCommandSlave();
        }
    }
}