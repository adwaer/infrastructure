using In.Cqrs.Query.Nats.Implementations;
using In.Cqrs.Query.Queries;
using In.Cqrs.Query.Simple;
using Microsoft.Extensions.DependencyInjection;

namespace In.Cqrs.Query.Nats.Config
{
    public static class IocConfig
    {
        public static IServiceCollection AddNatsQueryMaster(this IServiceCollection services)
        {
            return services
                .AddScoped<IQueryBuilder, QueryBuilder>()
                .AddScoped<IQueryFactory, NatsQueryFactory>()
                .AddSingleton(typeof(IQueryFor<>), typeof(QueryFor<>))
                .AddSingleton<INatsQueryReplyFactory, NatsQueryReplyFactory>();
        }

        public static IServiceCollection AddNatsQuerySlave(this IServiceCollection services)
        {
            return services.AddSingleton<INatsReceiverQueryQueueFactory, NatsReceiverQueryQueueFactory>()
                .AddSingleton<INatsQueryReplyFactory, NatsQueryReplyFactory>();
        }
    }
}