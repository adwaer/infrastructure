using System.Reflection;
using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.DDD.Config
{
    public class DddModuleBuilder : CoreModuleBuilder
    {
        private readonly Assembly[] _assemblies;

        public DddModuleBuilder(IServiceCollection services, Assembly[] assemblies)
            : base(services)
        {
            _assemblies = assemblies;
        }

        public override IServiceCollection AddServices()
        {
            return Collection.AddDddServices(_assemblies);
        }
    }
}