using In.Cqrs.Query.Nats.Implementations;
using In.Cqrs.Query.Queries;
using In.Cqrs.Query.Queries.Impls;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Query.Nats.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddNatsQueries(this IServiceCollection services)
        {
            return services.AddSingleton<INatsReceiverQueryQueueFactory, NatsReceiverQueryQueueFactory>()
                .AddScoped<IQueryBuilder, QueryBuilder>()
                .AddScoped<IQueryFactory, NatsQueryFactory>()
                .AddSingleton(typeof(IQueryFor<>), typeof(QueryFor<>))
                .AddSingleton<INatsQueryReplyFactory, NatsQueryReplyFactory>();
        }
    }
}