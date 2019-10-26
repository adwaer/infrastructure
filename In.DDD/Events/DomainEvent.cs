using System;
using System.Collections.Generic;

namespace In.DDD.Events
{
    public abstract class DomainEvent : IDomainMessage
    {
        public string Type => GetType().Name;

        public DateTime Created { get; }

        public Dictionary<string, Object> Args { get; }

        public string CorrelationId { get;  set; }

        public DomainEvent()
        {
            this.Created = DateTime.Now;
            this.Args = new Dictionary<string, Object>();
        }

        public abstract void Flatten();
    }
}