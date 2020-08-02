namespace In.DDD.Implementations
{
    public class SimpleDomainMessage<TAggregate> : IDomainMessage<TAggregate>
        where TAggregate : IAggregateRoot
    {
        public SimpleDomainMessage(TAggregate data)
        {
            Data = data;
        }

        public TAggregate Data { get; }
    }
}