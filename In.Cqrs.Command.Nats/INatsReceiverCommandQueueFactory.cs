using System.Collections.Generic;

namespace In.Cqrs.Command.Nats
{
    public interface INatsReceiverCommandQueueFactory
    {
        KeyValuePair<string, string> Get();
    }
}