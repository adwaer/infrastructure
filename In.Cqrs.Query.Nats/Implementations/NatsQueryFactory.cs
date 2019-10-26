using In.Common;
using In.Cqrs.Nats.Abstract;
using In.Cqrs.Query.Criterion.Abstract;
using In.Cqrs.Query.Nats.Adapters;
using In.Cqrs.Query.Queries;

namespace In.Cqrs.Query.Nats.Implementations
{
    public class NatsQueryFactory : IQueryFactory
    {
        private readonly INatsConnectionFactory _connectionFactory;
        private readonly INatsSerializer _serializer;
        private readonly ITypeFactory _typeFactory;
        private readonly INatsReceiverQueryQueueFactory _queueFactory;

        public NatsQueryFactory(INatsConnectionFactory connectionFactory, INatsSerializer serializer,
            ITypeFactory typeFactory, INatsReceiverQueryQueueFactory queueFactory)
        {
            _connectionFactory = connectionFactory;
            _serializer = serializer;
            _typeFactory = typeFactory;
            _queueFactory = queueFactory;
        }

        public IQueryHandler<TCriterion, TResult> Get<TCriterion, TResult>() where TCriterion : ICriterion
        {
            return new NatsQueryHandlerAdapter<TCriterion, TResult>(_connectionFactory, _serializer, _typeFactory, _queueFactory);
        }
    }
}