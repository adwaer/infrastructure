using In.Cqrs.Query.Queries.Generic;
using In.DataAccess.Entity.Abstract;

namespace In.Cqrs.Query.Queries
{
    /// <summary>
    /// Query builder
    /// </summary>
    public interface IQueryBuilder
    {
        /// <summary>
        /// Custom query factory
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        IQueryFor<TResult> For<TResult>();

        IGenericQueryBuilder<TSource> ForGeneric<TSource>()
            where TSource: class, IHasKey;
    }
}