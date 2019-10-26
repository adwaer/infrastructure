using In.DDD.Repository;
using In.Logging;

namespace In.DDD.Events
{
    public class DomainEventHandle<TDomainEvent> : IEventMsgHandle<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        readonly IDomainEventRepository _domainEventRepository;
        readonly IRequestCorrelationIdentifier _requestCorrelationIdentifier;

        public DomainEventHandle(IDomainEventRepository domainEventRepository, 
            IRequestCorrelationIdentifier requestCorrelationIdentifier)
        {
            _domainEventRepository = domainEventRepository;
            _requestCorrelationIdentifier = requestCorrelationIdentifier;
        }

        public void Handle(TDomainEvent @event)
        {
            @event.Flatten();
            @event.CorrelationId = _requestCorrelationIdentifier.CorrelationId;
            this._domainEventRepository.Add(@event);
        }
    }
}