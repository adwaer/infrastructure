using System.Collections.Generic;
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

        public IEnumerable<TEntity> Get(IExpressionCriterion<TEntity> condition)
        {
            var simpleQuery = _diScope.Resolve<ExpressionQuery>();
            return simpleQuery
                .Ask(condition);
        }

        public void Add(TEntity data)
        {
            var dataSetUow = _diScope.Resolve<IDataSetUow>();
            dataSetUow.Add(data);
        }

        public void Save(TEntity data)
        {
            var saveCommand = _diScope.Resolve<SaveCommandHandler<TEntity>>();
            saveCommand.Handle(data);
        }

        public void Remove(TEntity data)
        {
            var deleteCommand = _diScope.Resolve<DeleteCommandHandler<TEntity>>();
            deleteCommand.Handle(data);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _diScope
                .Resolve<IDataSetUow>()
                .Query<TEntity>();
        }
    }
}