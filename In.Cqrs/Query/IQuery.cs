using System.Threading.Tasks;

namespace In.Cqrs.Query
{
    /// <summary>
    ///     Интерфейс для объектов запросов к базе
    /// </summary>
    /// <typeparam name="TCriterion"> </typeparam>
    /// <typeparam name="TResult"> </typeparam>
    public interface IQuery<in TCriterion, TResult>
    {
        /// <summary>
        ///     Получить результат из базы
        /// </summary>
        /// <param name="criterion"> </param>
        /// <returns> </returns>
        Task<TResult> Ask(TCriterion criterion);
    }
}