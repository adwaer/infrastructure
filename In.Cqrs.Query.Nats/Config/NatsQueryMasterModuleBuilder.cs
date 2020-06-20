using In.AppBuilder;
using In.Cqrs.Nats.Config;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Query.Nats.Config
{
    public class NatsQueryMasterModuleBuilder : CoreModuleBuilder
    {
        private readonly NatsSenderOptions _natsSettings;

        public NatsQueryMasterModuleBuilder(IServiceCollection services, NatsSenderOptions natsSettings) :
            base(services)
        {
            _natsSettings = natsSettings;
        }

        public override IServiceCollection AddServices()
        {
            return Collection
                .AddNatsServices(_natsSettings)
                .AddNatsQueryMaster();
        }
    }
}