using System.Reflection;
using In.Common;
using In.DataAccess.EfCore.Implementations;
using In.Cqrs.Query.Queries.Generic;
using In.DataAccess.Repository;
using In.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace In.DataAccess.EfCore.Config
{
    public static class IocConfig
    {
        /// <summary>
        /// Dont forget init db provider!!!
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <typeparam name="TCtx"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddEfCoreServices<TCtx>(this IServiceCollection services,
            params Assembly[] assemblies) where TCtx: DbContext
        {
            return services
                .AddScoped<DbContext, TCtx>()
                .AddScoped<IDataSetUow, EfDatasetUow>()
                .AddTransient<ILinqProvider, EfLinqProvider>()
                .AddScoped(typeof(IRepository<>), typeof(SimpleRepository<>))
                .RegisterAssemblyImplementationsScoped(typeof(IRepository<>), assemblies)
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