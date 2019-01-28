using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data
{
    public static class MongoConnectionManager
    {
        private static MongoClient _client;
        private static string _database;

        public static string ConnectionString { get; set; }

        public static string Database {
            get => _database ?? (_database = (new MongoUrl(ConnectionString)).DatabaseName);
            set => _database = value;
        }

        public static MongoClient GetMongoClient()
        {
            return _client ?? (_client = new MongoClient(ConnectionString));
        }

        public static IMongoDatabase GetDatabase()
        {
            return GetMongoClient().GetDatabase(Database);
        }
    }
}