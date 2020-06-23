using System.Reflection;
using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Command.Simple.Config
{
    public class SimpleCommandModuleBuilder<TMsgResult> : CoreModuleBuilder where TMsgResult : class, IMessageResult
    {
        private readonly Assembly[] _assemblies;

        public SimpleCommandModuleBuilder(IServiceCollection services, params Assembly[] assemblies) : base(services)
        {
            _assemblies = assemblies;
        }

        public override IServiceCollection AddServices()
        {
            return Collection.AddCommandServices<TMsgResult>(_assemblies);
        }
    }
}