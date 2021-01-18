using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using In.DataAccess.Entity.Abstract;

namespace In.DataAccess.Repository.Abstract
{
    /// <summary>
    /// Query provider
    /// </summary>
    public interface IGreedyQueryProvider
    {
        /// <summary>
        /// Query object for concrete <see cref="IHasKey" />
        /// </summary>
        /// <typeparam name="TEntity">
        ///     <see cref="IHasKey" />
        /// </typeparam>
        /// <returns>
        ///     <see cref="IQueryable{TEntity}" /> object for type of TEntity
        /// </returns>
        IQueryable<TEntity> GetQuery<TEntity>()
            where TEntity : class;

        /// <summary>
        /// Execute and read result
        /// </summary>
        /// <param name="queryable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ICollection<T>> Get<T>(IQueryable<T> queryable);

        /// <summary>
        /// Execute and read first result
        /// </summary>
        /// <param name="queryable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetOne<T>(IQueryable<T> queryable);
    }
}