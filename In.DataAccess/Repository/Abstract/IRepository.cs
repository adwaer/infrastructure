using System.Collections.Generic;
using System.Threading.Tasks;
using In.Specifications;

namespace In.DataAccess.Repository.Abstract
{
    public interface IRepository<TResult>
    {
        Task<IEnumerable<TResult>> Find(Specification<TResult> specification);
        Task<TResult> FindOne(Specification<TResult> specification);
        Task<TResult[]> GetAll();
        
        void Add(TResult data);
        void Remove(TResult data);
        Task Save();
    }
}
