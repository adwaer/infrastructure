namespace In.Cqrs.Events
{
    public interface ISubscriber<in T>
    {
        void BeforeSave(T data);
        void AfterSave(T data);
        void BeforeDelete<TKey>(TKey id);
        void AfterDelete<TKey>(TKey id);
    }
}