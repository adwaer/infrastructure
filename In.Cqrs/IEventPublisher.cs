namespace In.Cqrs
{
    public interface IEventPublisher
    {
        void Publish<T>(T ev) where T : IEvent;
    }
}