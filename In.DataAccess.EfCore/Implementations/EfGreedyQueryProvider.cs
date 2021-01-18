using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using In.DataAccess.EfCore.Config;
using In.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace In.DataAccess.EfCore.Implementations
{
    public class EfGreedyQueryProvider : IGreedyQueryProvider
    {
        private readonly DbContext _dbContext;

        public EfGreedyQueryProvider(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetQuery<T>() where T : class
        {
            return _dbContext.Set<T>()
                .IncludeAllRecursively();
        }

        public async Task<ICollection<T>> Get<T>(IQueryable<T> queryable)
        {
            return await queryable.ToArrayAsync();
        }

        public Task<T> GetOne<T>(IQueryable<T> queryable)
        {
            return queryable.FirstOrDefaultAsync();
        }
    }
}