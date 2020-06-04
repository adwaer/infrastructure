using In.DDD.Repository;
using In.Logging;

namespace In.DDD.Events
{
    public class DomainEventHandle<TDomainEvent> : IEventMsgHandle<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        readonly IDomainEventRepository _domainEventRepository;

        public DomainEventHandle(IDomainEventRepository domainEventRepository)
        {
            _domainEventRepository = domainEventRepository;
        }

        public void Handle(TDomainEvent @event)
        {
            @event.Flatten();
            _domainEventRepository.Add(@event);
        }
    }
}