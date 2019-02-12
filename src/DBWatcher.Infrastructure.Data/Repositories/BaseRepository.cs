using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IMongoDatabase Database;
        protected readonly string CollectionName;

        protected BaseRepository(IMongoDatabase database, string collectionName)
        {
            Database = database;
            CollectionName = collectionName;
        }
    }

    public abstract class BaseRepository<T> : BaseRepository
    {
        protected IMongoCollection<T> GetCollection()
        {
            return Database.GetCollection<T>(CollectionName);
        }

        //protected BaseRepository(IMongoDatabase database) : base(database, typeof(T).Name) { }
        protected BaseRepository(IMongoDatabase database, string collectionName) : base(database, collectionName) { }
    }
}