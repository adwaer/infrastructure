using System.Reflection;
using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.DataMapping.Automapper.Config
{
    public class DataMappingAutomapperModuleBuilder : CoreModuleBuilder
    {
        private readonly Assembly[] _assemblies;

        public DataMappingAutomapperModuleBuilder(IServiceCollection services, Assembly[] assemblies)
            : base(services)
        {
            _assemblies = assemblies;
        }

        public override IServiceCollection AddServices()
        {
            return Collection.AddMappingServices(_assemblies);
        }
    }
}