namespace In.Di
{
    public interface IDiScope
    {
        T Resolve<T>();
    }
}