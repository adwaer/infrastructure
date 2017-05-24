using System.Collections.Generic;
using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs
{
    public interface IStorage<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(IExpressionCriterion<T> condition);
        void Add(T data);
        void Save(T data);
        void Remove(T data);
    }
}
