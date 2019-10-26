using System.Collections.Generic;
using System.Threading.Tasks;
using In.DataAccess.Entity.Abstract;
using In.DataAccess.Repository.Abstract;
using In.Specifications;

namespace In.Cqrs.Query.EfCore.Implementations
{
    public class DatasetCrudUow<TEntity> : IRepository<TEntity> where TEntity : class, IHasKey
    {
        private readonly IDataSetUow _dataSetUow;

        public DatasetCrudUow(IDataSetUow dataSetUow)
        {
            _dataSetUow = dataSetUow;
        }

        public async Task<IEnumerable<TEntity>> Find(Specification<TEntity> specification)
        {
            return await _dataSetUow.Find(specification.ToExpression());
        }

        public async Task<TEntity> FindOne(Specification<TEntity> specification)
        {
            return await _dataSetUow.FindOne(specification.ToExpression());
        }

        public void Add(TEntity data)
        {
            _dataSetUow.AddEntity(data);
        }

        public void Remove(TEntity data)
        {
            _dataSetUow.RemoveEntity(data);
        }

        public async Task Save()
        {
            await _dataSetUow.CommitAsync();
        }

    }
}