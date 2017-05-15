using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace In.Entity.Uow
{
    public interface IDataSetUow
    {
        IQueryable<T> Query<T>() where T : class;
        IQueryable Query(Type type);
        void Include<T, TProp>(IQueryable<T> queryable, params Expression<Func<T, TProp>>[] paths) where T : class;
        object GetContext();
        TEntity Find<TEntity>(object id) where TEntity : class;

        /* CRUD */
        void Add<T>(T entity) where T : class;
        void AddRange<T>(IEnumerable<T> entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void RemoveRange<T>(IEnumerable<T> entity) where T : class;
        int Commit();
        Task<int> CommitAsync();
        void FixupState<T>(T entity) where T : class;
    }
}
