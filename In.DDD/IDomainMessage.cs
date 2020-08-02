namespace In.DDD
{
    public interface IDomainMessage<out TAggregate>
        where TAggregate : IAggregateRoot
    {
        TAggregate Data { get; }
    }
}