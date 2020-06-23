using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Query.Nats.Config
{
    public class NatsQueryMasterModuleBuilder : CoreModuleBuilder
    {
        public NatsQueryMasterModuleBuilder(IServiceCollection services) :
            base(services)
        {
        }

        public override IServiceCollection AddServices()
        {
            return Collection
                .AddNatsQueryMaster();
        }
    }
}