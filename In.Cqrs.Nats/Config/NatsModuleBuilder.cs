using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Nats.Config
{
    public class NatsModuleBuilder: CoreModuleBuilder
    {
        private readonly NatsSettings _natsSettings;

        public NatsModuleBuilder(IServiceCollection services, NatsSettings natsSettings)
            : base(services)
        {
            _natsSettings = natsSettings;
        }

        public override IServiceCollection AddServices()
        {
            return Collection
                .AddNatsServices(_natsSettings);
        }
    }
}