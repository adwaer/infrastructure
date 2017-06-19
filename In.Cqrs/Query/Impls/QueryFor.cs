using System.Threading;
using System.Threading.Tasks;
using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs.Query.Impls
{
    /// <summary>
    ///     Стандартная реализация <see cref="IQueryFor{T}" />
    /// </summary>
    /// <typeparam name="TResult">Результат возвращаемый запросом</typeparam>
    public class QueryFor<TResult> : IQueryFor<TResult>
    {
        private readonly IQueryFactory _factory;

        /// <summary>
        ///     ctor
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
                .Create<TCriterion, TResult>()
                .Ask(criterion);
        }
    }
}