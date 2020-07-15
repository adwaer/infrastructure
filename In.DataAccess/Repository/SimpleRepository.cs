using System.Collections.Generic;
using System.Threading.Tasks;
using In.DataAccess.Entity.Abstract;
using In.DataAccess.Repository.Abstract;
using In.Specifications;

namespace In.DataAccess.Repository
{
    public class SimpleRepository<TEntity> : IRepository<TEntity> where TEntity : class, IHasKey
    {
        private readonly IDataSetUow _dataSetUow;

        public SimpleRepository(IDataSetUow dataSetUow)
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

        public Task<TEntity[]> GetAll()
        {
            return _dataSetUow.GetAll<TEntity>();
        }

        public void Add(TEntity data)
        {
            _dataSetUow.AddEntity(data);
        }

        public void Update(TEntity entity)
        {
            _dataSetUow.Update(entity);
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