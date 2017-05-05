using System;

namespace In.Cqrs.Events
{
    internal struct EventDescriptor
    {

        public readonly IEvent EventData;
        public readonly Guid Id;
        public readonly int Version;

        public EventDescriptor(Guid id, IEvent eventData, int version)
        {
            EventData = eventData;
            Version = version;
            Id = id;
        }
    }
}
