﻿using System.Threading.Tasks;
using In.Common;

namespace In.DDD.Implementations
{
    public class SimpleDomainMessageDispatcher<TAggregate>: IDomainMessageDispatcher<TAggregate> where TAggregate : IAggregateRoot
    {
        private readonly IDiScope _diScope;

        public SimpleDomainMessageDispatcher(IDiScope diScope)
        {
            _diScope = diScope;
        }

        public async Task Dispatch(IDomainMessage<TAggregate> message)
        {
            var handlers = _diScope.ResolveAll<IDomainMessageHandler<TAggregate>>();
            foreach (var handler in handlers)
            {
                await handler.Handle(message.Data);
            }
        }
    }
}