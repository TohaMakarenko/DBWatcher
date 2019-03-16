using DBWatcher.Core;
using DBWatcher.Core.Queue;
using DBWatcher.Core.Repositories;
using DBWatcher.Infrastructure.Data.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly IMongoDatabase Database;

        public IMessageBus Bus { get; }
        
        public IScriptRepository ScriptRepository { get; }
        public IConnectionPropertiesRepository ConnectionPropertiesRepository { get; }

        public UnitOfWork(string connectionString, IMessageBus bus) : this(new MongoUrl(connectionString), bus) { }

        public UnitOfWork(MongoUrl connectionUrl, IMessageBus bus)
        {
            Bus = bus;
            var client = new MongoClient(connectionUrl);
            Database = client.GetDatabase(connectionUrl.DatabaseName);

            ScriptRepository = new ScriptRepository(Database);
            ConnectionPropertiesRepository = new ConnectionPropertiesRepository(Database);
        }
    }
}