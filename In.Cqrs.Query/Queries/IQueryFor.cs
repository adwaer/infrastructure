using System.Threading.Tasks;
using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs.Query.Queries
{
    /// <summary>
    /// Interface for specifying query criteria
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    public interface IQueryFor<T>
    {
        /// <summary>
        /// Add criterion
        /// </summary>
        /// <param name="criterion"></param>
        /// <typeparam name="TCriterion"></typeparam>
        /// <returns></returns>
        T With<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion;

        Task<T> WithAsync<TCriterion>(TCriterion criterion)
            where TCriterion : ICriterion;
    }
}