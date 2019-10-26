using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs.Query.Queries
{
    /// <summary>
    /// Query factory
    /// </summary>
    public interface IQueryFactory
    {
        /// <summary>
        /// Create a query returning the desired results with specific criteria
        /// </summary>
        /// <typeparam name="TCriterion"> </typeparam>
        /// <typeparam name="TResult"> </typeparam>
        /// <returns> </returns>
        IQueryHandler<TCriterion, TResult> Get<TCriterion, TResult>()
            where TCriterion : ICriterion;
    }
}