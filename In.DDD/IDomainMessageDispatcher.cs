using System.Threading.Tasks;

namespace In.DDD
{
    public interface IDomainMessageDispatcher<in TAggregate> where TAggregate : IAggregateRoot
    {
        Task Dispatch(IDomainMessage<TAggregate> message);
    }
}