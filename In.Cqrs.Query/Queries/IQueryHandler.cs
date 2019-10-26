using System.Threading.Tasks;
using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs.Query.Queries
{
    /// <summary>
    /// Query interface
    /// </summary>
    /// <typeparam name="TCriterion"> </typeparam>
    /// <typeparam name="TResult"> </typeparam>
    public interface IQueryHandler<in TCriterion, TResult> where TCriterion : ICriterion
    {
        /// <summary>
        /// Ask
        /// </summary>
        /// <param name="criterion"> </param>
        /// <returns> </returns>
        Task<TResult> Ask(TCriterion criterion);
    }
}