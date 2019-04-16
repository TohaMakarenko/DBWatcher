using DBWatcher.Core;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Queue;
using DBWatcher.Core.Repositories;
using DBWatcher.Infrastructure.Data.Repositories;
using MongoDB.Driver;

namespace DBWatcher.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly IMongoDatabase Database;

        public UnitOfWork(string connectionString, IMessageBroker broker) :
            this(new MongoUrl(connectionString), broker) { }

        public UnitOfWork(MongoUrl connectionUrl, IMessageBroker broker)
        {
            Broker = broker;
            var client = new MongoClient(connectionUrl);
            Database = client.GetDatabase(connectionUrl.DatabaseName);

            ScriptRepository = new ScriptRepository(Database);
            ConnectionPropertiesRepository = new ConnectionPropertiesRepository(Database);
            JobRepository = new JobRepository(Database);
        }

        public IMessageBroker Broker { get; }

        public IScriptRepository ScriptRepository { get; }
        public IConnectionPropertiesRepository ConnectionPropertiesRepository { get; }
        public IJobRepository JobRepository { get; }

        public IRepository<T, TKey> GetRepository<T, TKey>() where T : BaseDto<TKey>
        {
            return new BaseRepository<T, TKey>(Database);
        }

        public IRepository<T, TKey> GetRepository<T, TKey>(string collectionName) where T : BaseDto<TKey>
        {
            return new BaseRepository<T, TKey>(Database, collectionName);
        }
    }
}