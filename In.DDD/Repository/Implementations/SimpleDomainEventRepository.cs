using System.Collections.Generic;
using System.Linq;
using In.DDD.Events;

namespace In.DDD.Repository.Implementations
{
    public class SimpleDomainEventRepository : IDomainEventRepository
    {
        private readonly List<DomainEventRecord> _domainEvents = new List<DomainEventRecord>();

        public void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent
        {
            _domainEvents.Add(
                new DomainEventRecord()
                {
                    Created = domainEvent.Created,
                    Type = domainEvent.Type,
                    Args = domainEvent.Args
                        .Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value.ToString()))
                        .ToList(),
                    CorrelationID = domainEvent.CorrelationId
                });
        }

        public IEnumerable<DomainEventRecord> FindAll()
        {
            return _domainEvents;
        }
    }
}