using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using In.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace In.DataAccess.EfCore.Implementations
{
    public class EfDatasetUow : IDataSetUow
    {
        private readonly DbContext _dbContext;

        public EfDatasetUow(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<T>> Find<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await _dbContext.Set<T>()
                .Where(expression)
                .ToArrayAsync();
        }

        public Task<T> FindOne<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return _dbContext.Set<T>()
                .FirstOrDefaultAsync(expression);
        }

        public Task<T[]> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>()
                .ToArrayAsync();
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void AddEntity<T>(T entity) where T : class
        {
            _dbContext.Add(entity);
        }

        public void AddRange<T>(IEnumerable<T> entity) where T : class
        {
            _dbContext.AddRange(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Update(entity);
        }

        public void RemoveEntity<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);
        }

        public void RemoveRange<T>(IEnumerable<T> entity) where T : class
        {
            _dbContext.RemoveRange(entity);
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
    }
}