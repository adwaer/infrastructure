using System.Linq;
using In.DataAccess.Entity.Abstract;
using In.DataAccess.Repository.Abstract;
using MongoDB.Driver;

namespace In.DataAccess.Mongo
{
    public class MongoLinqProvider : ILinqProvider
    {
        private readonly IMongoDatabase _database;

        public MongoLinqProvider(IMongoDatabase database)
        {
            _database = database;
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class, IHasKey
        {
            var mongoCollection = _database.GetCollection<TEntity>(nameof(TEntity));
            return mongoCollection.AsQueryable();
        }
    }
}