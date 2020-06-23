using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Command.Nats.Config
{
    public class NatsCommandMasterModuleBuilder<TMsgResult> : CoreModuleBuilder where TMsgResult : class, IMessageResult
    {
        public NatsCommandMasterModuleBuilder(IServiceCollection services) :
            base(services)
        {
        }

        public override IServiceCollection AddServices()
        {
            return Collection
                .AddNatsCommandMaster<TMsgResult>();
        }
    }
}