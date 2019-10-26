using System;

namespace In.DDD
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}