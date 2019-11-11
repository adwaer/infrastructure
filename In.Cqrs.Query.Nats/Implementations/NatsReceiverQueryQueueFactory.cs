namespace In.Cqrs.Query.Nats.Implementations
{
    public class NatsReceiverQueryQueueFactory : INatsReceiverQueryQueueFactory
    {
        public string Get() => "QuerySubject";
    }
}