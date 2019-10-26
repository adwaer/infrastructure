namespace In.DDD.Events
{
    public interface IEventMsgHandle<T>
        where T : IDomainMessage
    {
        void Handle(T args); 
    }
}