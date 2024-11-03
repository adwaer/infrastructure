using System.Threading.Tasks;
using In.DataAccess.Entity.Abstract;

namespace In.DDD
{
    public interface IDomainUow<TAggregate, TModel>
        where TModel : class, IHasKey, new()
        where TAggregate : class, IAggregateRoot<TModel>, new()
    {
        IDomainRepository<TAggregate, TModel> Repository { get; }
        Task Commit();
    }
}