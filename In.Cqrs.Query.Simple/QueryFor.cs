using System.Threading.Tasks;
using In.Common;
using In.Cqrs.Query.Criterion.Abstract;
using In.Cqrs.Query.Queries;

namespace In.Cqrs.Query.Simple
{
    /// <summary>
    ///  Simple implementation
    /// </summary>
    /// <typeparam name="TResult">Response type</typeparam>
    public class QueryFor<TResult> : IQueryFor<TResult>
    {
        private readonly IQueryFactory _factory;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="factory"></param>
        public QueryFor(IQueryFactory factory)
        {
            _factory = factory;
        }

        public TResult With<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion
        {
            return AsyncHelpers.RunSync(() => WithAsync(criterion));
        }

        public async Task<TResult> WithAsync<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion
        {
            return await _factory
                .Get<TCriterion, TResult>()
                .Ask(criterion);
        }
    }
}