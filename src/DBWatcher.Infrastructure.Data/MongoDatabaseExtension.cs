using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data
{
    public static class MongoDatabaseExtension
    {
        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase database)
        {
            return database.GetCollection<T>(typeof(T).Name);
        }
    }
}