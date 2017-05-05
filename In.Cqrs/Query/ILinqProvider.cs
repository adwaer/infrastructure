using System.Linq;
using In.Domain;

namespace In.Cqrs.Query
{
    /// <summary>
    /// </summary>
    public interface ILinqProvider
    {
        /// <summary>
        ///     Query object for concrete <see cref="IEntity" />
        /// </summary>
        /// <typeparam name="TEntity">
        ///     <see cref="IEntity" />
        /// </typeparam>
        /// <returns>
        ///     <see cref="IQueryable{TEntity}" /> object for type of TEntity
        /// </returns>
        IQueryable<TEntity> Query<TEntity>()
            where TEntity : class, IEntity, new();
    }
}