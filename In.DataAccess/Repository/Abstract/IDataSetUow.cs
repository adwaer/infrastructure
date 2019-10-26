using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace In.DataAccess.Repository.Abstract
{
    public interface IDataSetUow
    {
        Task<T[]> Find<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> FindOne<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<int> CommitAsync();

        /* CRUD */
        void AddEntity<T>(T entity) where T : class;
        void AddRange<T>(IEnumerable<T> entity) where T : class;
        void RemoveEntity<T>(T entity) where T : class;
        void RemoveRange<T>(IEnumerable<T> entity) where T : class;
        int Commit();
    }
}
