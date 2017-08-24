using System.Linq;
using System.Threading.Tasks;
using In.Cqrs.Command;
using In.Cqrs.Query.Criterion.Abstract;
using In.Cqrs.Query;
using In.Di;
using In.Domain;
using In.Entity.Uow;

namespace In.Cqrs.Storage
{
    public class SimpleStorage<TEntity> : IStorage<TEntity> where TEntity : class, IEntity
    {
        private readonly IDiScope _diScope;

        public SimpleStorage(IDiScope diScope/*<ExpressionQuery> simpleQuery, SaveCommandHandler<TEntity, TKey> saveCommand, DeleteCommandHandler<TEntity, TKey> deleteCommand, IDataSetUow dataSetUow*/)
        {
            _diScope = diScope;
        }

        public IQueryable<TEntity> Get(IExpressionCriterion<TEntity> condition)
        {
            var simpleQuery = _diScope.Resolve<ExpressionQuery>();
            return simpleQuery
                .Ask(condition);
        }
        public IQueryable<TEntity> GetAll()
        {
            return _diScope
                .Resolve<IDataSetUow>()
                .Query<TEntity>();
        }

        public void Add(TEntity data)
        {
            var dataSetUow = _diScope.Resolve<IDataSetUow>();
            dataSetUow.Add(data);
        }

        public async Task Save(params TEntity[] messages)
        {
            var saveCommand = _diScope.Resolve<SaveCommandHandler<TEntity>>();
            await saveCommand.Handle(messages);
        }

        public void Remove(TEntity data)
        {
            var dataSetUow = _diScope.Resolve<IDataSetUow>();

            var command = new DeleteCommand<TEntity>(data);
            AsyncHelpers.RunSync(() => new DeleteCommandHandler<TEntity>(dataSetUow).Handle(command));

            //var msgSender = _diScope.Resolve<IMessageSender>();
            //msgSender.Send(new DeleteCommand<TEntity>(data));
        }

    }
}