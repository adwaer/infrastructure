using ByndyuSoft.Infrastructure.Domain;

namespace In.Cqrs.Query
{
    /// <summary>
    ///     Интерфейс для построителя запросов
    /// </summary>
    public interface IQueryBuilder
    {
        /// <summary>
        ///     Начать строить запрос для результата
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        IQueryFor<TResult> For<TResult>();
    }
}