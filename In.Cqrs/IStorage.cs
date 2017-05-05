using System.Collections.Generic;
using System.Threading.Tasks;
using In.Cqrs.Condition.Abstract;

namespace In.Cqrs
{
    public interface IStorage<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(IExpressionCriterion<T> condition);
        void Add(T data);
        void Save(T data);
        void Remove(T data);
    }
}
