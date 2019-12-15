using MongoDB.Driver;

namespace In.DataAccess.Mongo
{
    public interface IMongoCtx
    {
        IMongoDatabase Db { get; }
    }
}