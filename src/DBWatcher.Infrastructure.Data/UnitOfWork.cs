using AutoMapper;
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

        public UnitOfWork(string connectionString, IMessageBroker broker, IMapper mapper) :
            this(new MongoUrl(connectionString), broker, mapper) { }

        public UnitOfWork(MongoUrl connectionUrl, IMessageBroker broker, IMapper mapper)
        {
            Broker = broker;
            var client = new MongoClient(connectionUrl);
            Database = client.GetDatabase(connectionUrl.DatabaseName);

            ScriptRepository = new ScriptRepository(Database);
            ConnectionPropertiesRepository = new ConnectionPropertiesRepository(Database);
            JobRepository = new JobRepository(Database);
            JobLogRepository = new JobLogRepository(Database, mapper);
            DashboardRepository = new DashboardRepository(Database);
            FolderRepository = new FolderRepository(Database);
        }

        public IMessageBroker Broker { get; }

        public IScriptRepository ScriptRepository { get; }
        public IConnectionPropertiesRepository ConnectionPropertiesRepository { get; }
        public IJobRepository JobRepository { get; }
        public IJobLogRepository JobLogRepository { get; }
        public IDashboardRepository DashboardRepository { get; }
        public IFolderRepository FolderRepository { get; }

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