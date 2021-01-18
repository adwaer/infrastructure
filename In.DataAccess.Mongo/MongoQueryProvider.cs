using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using In.DataAccess.Entity.Abstract;
using In.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace In.DataAccess.Mongo
{
    public class MongoQueryProvider : ISimpleQueryProvider, IGreedyQueryProvider
    {
        private readonly IMongoDatabase _database;

        public MongoQueryProvider(IMongoCtx ctx)
        {
            _database = ctx.Db;
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class
        {
            return _database
                .GetCollection<TEntity>(nameof(TEntity))
                .AsQueryable();
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