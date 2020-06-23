using System.Reflection;
using In.AppBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace In.DataAccess.Mongo.Config
{
    public class DataAccessMongoModuleBuilder : CoreModuleBuilder
    {
        private readonly Assembly[] _assemblies;

        public DataAccessMongoModuleBuilder(IServiceCollection services, params Assembly[] assemblies)
            : base(services)
        {
            _assemblies = assemblies;
        }

        public override IServiceCollection AddServices()
        {
            return Collection.AddMongoServices(_assemblies);
        }
    }
}