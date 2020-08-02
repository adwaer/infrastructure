using System.Threading.Tasks;
using In.DataAccess.Entity.Abstract;

namespace In.DDD
{
    public interface IDomainUow<TAggregate, TModel>
        where TModel : class, IHasKey
        where TAggregate : IAggregateRoot<TModel>
    {
        IDomainRepository<TAggregate, TModel> Repository { get; }
        Task Commit();
    }
}