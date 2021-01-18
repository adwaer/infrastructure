using System.Linq;
using In.DataAccess.Entity.Abstract;

namespace In.DataAccess.Repository.Abstract
{
    /// <summary>
    /// Query provider
    /// </summary>
    public interface ISimpleQueryProvider
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
    }
}