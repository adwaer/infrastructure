using System.Threading.Tasks;
using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs.Query
{
    /// <summary>
    ///     Интерфейс для задания критериев запроса
    /// </summary>
    /// <typeparam name="T">Тип возращаемого запросом значения</typeparam>
    public interface IQueryFor<T>
    {
        /// <summary>
        ///     Добавить критерии запроса
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