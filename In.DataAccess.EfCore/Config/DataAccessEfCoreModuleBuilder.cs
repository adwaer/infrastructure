using System.Reflection;
using In.AppBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace In.DataAccess.EfCore.Config
{
    public class DataAccessEfCoreModuleBuilder<TCtx> : CoreModuleBuilder where TCtx : DbContext
    {
        private readonly Assembly[] _assemblies;

        public DataAccessEfCoreModuleBuilder(IServiceCollection services, Assembly[] assemblies)
            : base(services)
        {
            _assemblies = assemblies;
        }

        public override IServiceCollection AddServices()
        {
            return Collection.AddEfCoreServices<TCtx>(_assemblies);
        }
    }
}