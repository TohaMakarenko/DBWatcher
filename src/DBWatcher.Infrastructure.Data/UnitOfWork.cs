using DBWatcher.Core.Repositories;
using DBWatcher.Infrastructure.Data.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly IMongoDatabase Database;

        public IScriptRepository ScriptRepository { get; }
        public IConnectionPropertiesRepository ConnectionPropertiesRepository { get; }

        public UnitOfWork(string connectionString) : this(new MongoUrl(connectionString)) { }

        public UnitOfWork(MongoUrl connectionUrl)
        {
            var client = new MongoClient(connectionUrl);
            Database = client.GetDatabase(connectionUrl.DatabaseName);

            ScriptRepository = new ScriptRepository(Database);
            ConnectionPropertiesRepository = new ConnectionPropertiesRepository(Database);
        }
    }
}