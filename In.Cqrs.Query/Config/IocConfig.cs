using System.Reflection;
using In.Common;
using In.Cqrs.Query.Queries;
using In.Cqrs.Query.Queries.Impls;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Query.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddQueryServices(this IServiceCollection services, Assembly[] assemblies)
        {
            return services.AddScoped<IQueryBuilder, QueryBuilder>()
                .AddScoped<IQueryFactory, QueryFactory>()
                .AddSingleton(typeof(IQueryFor<>), typeof(QueryFor<>))
                .RegisterAssemblyImplementationsScoped(assemblies, typeof(IQueryHandler<,>));
        }
    }
}