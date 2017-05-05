namespace In.Domain
{
    public interface IDiScope
    {
        T Resolve<T>();
    }
}