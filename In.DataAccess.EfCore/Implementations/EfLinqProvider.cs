using System.Linq;
using In.DataAccess.Entity.Abstract;
using In.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace In.DataAccess.EfCore.Implementations
{
    public class EfLinqProvider : ILinqProvider
    {
        private readonly DbContext _dbContext;

        public EfLinqProvider(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class, IHasKey
        {
            return _dbContext.Set<TEntity>()
                .AsNoTracking();
        }
    }
}