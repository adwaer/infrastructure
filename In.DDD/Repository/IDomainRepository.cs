using In.DataAccess.Repository.Abstract;

namespace In.DDD.Repository
{
    public interface IDomainRepository<TAggregate> : IRepository<TAggregate>
        where TAggregate: IAggregateRoot
    {
        
    }
}