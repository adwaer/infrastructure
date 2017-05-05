namespace In.Cqrs.Events.Impl
{
    public class SimpleSubscriber<T> : ISubscriber<T>
    {
        public virtual void BeforeSave(T data)
        {
        }

        public virtual void AfterSave(T data)
        {
        }

        public virtual void BeforeDelete<TKey>(TKey id)
        {
        }

        public virtual void AfterDelete<TKey>(TKey id)
        {
        }
        
    }
}
