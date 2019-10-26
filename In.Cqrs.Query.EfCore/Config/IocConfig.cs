using System.Reflection;
using In.Common;
using In.Cqrs.Query.EfCore.Implementations;
using In.Cqrs.Query.Queries.Generic;
using In.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Query.EfCore.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddEfCore<TCtx>(this IServiceCollection services,
            Assembly[] assemblies) where TCtx: DbContext
        {
            return services
                .AddScoped<DbContext, TCtx>()
                .AddScoped<IDataSetUow, EfDatasetUow>()
                .AddScoped<ILinqProvider, EfLinqProvider>()
                .AddScoped(typeof(IRepository<>), typeof(DatasetCrudUow<>))
                .RegisterAssemblyImplementationsScoped(assemblies, typeof(IRepository<>))
                .AddTransient(typeof(IGenericQueryBuilder<>), typeof(GenericQueryBuilder<>))
                .Scan(scan => scan
                    .FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(GenericQueryBuilder<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                );
        }
    }
}