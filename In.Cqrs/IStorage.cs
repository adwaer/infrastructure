using System.Collections.Generic;
using System.Threading.Tasks;
using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs
{
    public interface IStorage<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(IExpressionCriterion<T> condition);
        void Add(T data);
        void Remove(T data);
        Task Save(params T[] messages);
    }
}
