using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using In.DataAccess.Entity.Abstract;
using In.DataAccess.Repository.Abstract;
using In.Specifications;

namespace In.DDD.Implementations
{
    public class SimpleDomainRepository<TAggregate, TModel> : IDomainRepository<TAggregate, TModel>
        where TModel : class, IHasKey, new()
        where TAggregate : class, IAggregateRoot<TModel>, new()
    {
        private readonly IGreedyQueryProvider _queryProvider;
        private readonly IRepository<TModel> _repository;
        private readonly List<IDomainMessage<TAggregate>> _domainMessages;

        public SimpleDomainRepository(IGreedyQueryProvider queryProvider, IRepository<TModel> repository)
        {
            _repository = repository;
            _queryProvider = queryProvider;
            _domainMessages = new List<IDomainMessage<TAggregate>>();
        }

        public async Task<IEnumerable<TAggregate>> Find(Specification<TModel> specification)
        {
            var models = await _queryProvider.Get(
                _queryProvider.GetQuery<TModel>()
                    .Where(specification.ToExpression())
            );

            return models.Select(model => MakeAggregateRoot(model));
        }

        public async Task<TAggregate> FindOne(Specification<TModel> specification)
        {
            var model = await _queryProvider.GetOne(
                _queryProvider.GetQuery<TModel>()
                    .Where(specification.ToExpression())
            );

            return model == null ? null : MakeAggregateRoot(model);
        }

        public async Task<TAggregate[]> GetAll()
        {
            return (await _queryProvider.Get(
                    _queryProvider.GetQuery<TModel>()
                ))
                .Select(model => MakeAggregateRoot(model))
                .ToArray();
        }

        public IEnumerable<IDomainMessage<TAggregate>> GetDomainMessages()
        {
            var result = _domainMessages.ToList()
                .AsReadOnly();

            _domainMessages.Clear();

            return result;
        }

        public TAggregate Create()
        {
            return Add(new TModel());
        }

        public TAggregate Add(TModel model)
        {
            var aggr = MakeAggregateRoot(model, true);

            _repository.Add(model);
            _domainMessages.Add(new SimpleDomainMessage<TAggregate>(aggr));

            return aggr;
        }

        public TAggregate Update(TAggregate aggregate)
        {
            _repository.Update(aggregate.Model);
            _domainMessages.Add(new SimpleDomainMessage<TAggregate>(aggregate));

            return aggregate;
        }

        private TAggregate MakeAggregateRoot(TModel entity, bool isNew = false)
        {
            var aggr = new TAggregate();
            aggr.SetModel(entity, isNew);

            return aggr;
        }
    }
}