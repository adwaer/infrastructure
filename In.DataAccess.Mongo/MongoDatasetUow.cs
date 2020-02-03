using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using In.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace In.DataAccess.Mongo
{
    public class MongoDatasetUow : IDataSetUow
    {
        private readonly IMongoDatabase _database;
        private readonly IClientSessionHandle _session;

        public MongoDatasetUow(IMongoCtx ctx)
        {
            _database = ctx.Db;
            _session = _database.Client.StartSession();
        }

        public async Task<ICollection<T>> Find<T>(Expression<Func<T, bool>> expression) where T : class
        {
            using var cursor = await _database.GetCollection<T>(nameof(T))
                .FindAsync(expression);
            return await cursor.ToListAsync();
        }

        public async Task<T> FindOne<T>(Expression<Func<T, bool>> expression) where T : class
        {
            using var cursor = await _database.GetCollection<T>(nameof(T))
                .FindAsync(expression);
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<T[]> GetAll<T>() where T : class
        {
            return await _database
                .GetCollection<T>(nameof(T))
                .AsQueryable()
                .ToArrayAsync();
        }

        public async Task<int> CommitAsync()
        {
            await _session.CommitTransactionAsync();
            return 0;
        }

        public void AddEntity<T>(T entity) where T : class
        {
            _database.GetCollection<T>(nameof(T))
                .InsertOne(entity);
        }

        public void AddRange<T>(IEnumerable<T> entity) where T : class
        {
            _database.GetCollection<T>(nameof(T))
                .InsertMany(entity);
        }

        public void RemoveEntity<T>(T entity) where T : class
        {
            _database.GetCollection<T>(nameof(T))
                .FindOneAndDelete(arg => GetId(entity) == GetId(arg));
        }

        public void RemoveRange<T>(IEnumerable<T> entity) where T : class
        {
            foreach (var e in entity)
            {
                RemoveEntity(e);
            }
        }

        public int Commit()
        {
            _session.CommitTransaction();
            return 0;
        }
        
        private static object GetId(object src)
        {
            return src.GetType()
                .GetProperty("Id")
                ?.GetValue(src, null);
        }
    }
}