using System.Reflection;
using In.Common;
using In.Cqrs.Query.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Query.Simple.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddQueryServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.AddTransient<IQueryBuilder, QueryBuilder>()
                .AddTransient<IQueryFactory, QueryFactory>()
                .AddSingleton(typeof(IQueryFor<>), typeof(QueryFor<>))
                .RegisterAssemblyImplementationsTransient(typeof(IQueryHandler<,>), assemblies);
        }
    }
}