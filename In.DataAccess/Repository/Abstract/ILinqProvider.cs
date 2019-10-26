using System.Linq;
using In.DataAccess.Entity.Abstract;

namespace In.DataAccess.Repository.Abstract
{
    /// <summary>
    /// Linq provider
    /// </summary>
    public interface ILinqProvider
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
            where TEntity : class, IHasKey;
    }
}