using In.Cqrs.Query.Nats;

namespace In.Cqrs.Nats
{
    public class NatsReceiverQueryQueueFactory : INatsReceiverQueryQueueFactory
    {
        public string Get() => "QuerySubject";
    }
}