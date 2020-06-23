using System.Reflection;
using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Query.Simple.Config
{
    public class SimpleQueryModuleBuilder : CoreModuleBuilder
    {
        private readonly Assembly[] _assemblies;

        public SimpleQueryModuleBuilder(IServiceCollection services, params Assembly[] assemblies) : base(services)
        {
            _assemblies = assemblies;
        }

        public override IServiceCollection AddServices()
        {
            return Collection.AddQueryServices(_assemblies);
        }
    }
}