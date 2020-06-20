using In.AppBuilder;
using In.Cqrs.Nats.Config;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Command.Nats.Config
{
    public class NatsCommandMasterModuleBuilder<TMsgResult> : CoreModuleBuilder where TMsgResult : class, IMessageResult
    {
        private readonly NatsSenderOptions _natsSettings;

        public NatsCommandMasterModuleBuilder(IServiceCollection services, NatsSenderOptions natsSettings) :
            base(services)
        {
            _natsSettings = natsSettings;
        }

        public override IServiceCollection AddServices()
        {
            return Collection
                .AddNatsServices(_natsSettings)
                .AddNatsCommandMaster<TMsgResult>();
        }
    }
}