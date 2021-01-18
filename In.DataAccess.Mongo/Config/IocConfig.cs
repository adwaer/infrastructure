using System.Reflection;
using In.Common;
using In.Cqrs.Query.Queries.Generic;
using In.DataAccess.EfCore;
using In.DataAccess.Repository;
using In.DataAccess.Repository.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace In.DataAccess.Mongo.Config
{
    public static class IocConfig
    {
        /// <summary>
        /// Dont forget init db provider!!!
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoServices(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            return services
                .AddTransient<IDataSetUow, MongoDatasetUow>()
                .AddTransient<ISimpleQueryProvider, MongoQueryProvider>()
                .AddTransient<IGreedyQueryProvider, MongoQueryProvider>()
                .AddTransient(typeof(IRepository<>), typeof(SimpleRepository<>))
                .RegisterAssemblyImplementationsTransient(typeof(IRepository<>), assemblies)
                .AddTransient(typeof(IGenericQueryBuilder<>), typeof(GenericQueryBuilder<>))
                .Scan(scan => scan
                    .FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(GenericQueryBuilder<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                );
        }
    }
}