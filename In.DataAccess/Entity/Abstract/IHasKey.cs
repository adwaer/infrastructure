namespace In.DataAccess.Entity.Abstract
{
    public interface IHasKey
    {
    }

    public interface IHasKey<out TId> : IHasKey
    {
        TId Id { get; }
    }
}
