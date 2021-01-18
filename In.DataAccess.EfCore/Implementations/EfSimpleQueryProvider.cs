using System.Linq;
using In.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace In.DataAccess.EfCore.Implementations
{
    public class EfSimpleQueryProvider : ISimpleQueryProvider
    {
        private readonly DbContext _dbContext;

        public EfSimpleQueryProvider(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>()
                .AsNoTracking();
        }
    }
}