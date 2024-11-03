using System.Collections.Generic;
using System.Threading.Tasks;
using In.DataAccess.Entity.Abstract;
using In.Specifications;

namespace In.DDD
{
    public interface IDomainRepository<TAggregate, TModel>
        where TModel : class, IHasKey, new()
        where TAggregate : class, IAggregateRoot<TModel>, new()
    {
        Task<IEnumerable<TAggregate>> Find(Specification<TModel> specification);

        Task<TAggregate> FindOne(Specification<TModel> specification);

        Task<TAggregate[]> GetAll();

        IEnumerable<IDomainMessage<TAggregate>> GetDomainMessages();

        TAggregate Create();
        TAggregate Add(TModel aggregate);

        TAggregate Update(TAggregate aggregate);
    }
}