using System.Collections.Generic;

namespace In.Cqrs.Command.Nats.Implementations
{
    public class NatsReceiverCommandQueueFactory : INatsReceiverCommandQueueFactory
    {
        public KeyValuePair<string, string> Get() => new KeyValuePair<string, string>("ComandsSubject", "ComandsQueue");
    }
}