﻿using System.Threading.Tasks;

namespace In.DDD
{
    public interface IDomainMessageHandler<in TAggregate> where TAggregate : IAggregateRoot
    {
        Task Handle(TAggregate args);
    }
}