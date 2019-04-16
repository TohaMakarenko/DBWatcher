using DBWatcher.Core.Dto;
using DBWatcher.Core.Queue;
using DBWatcher.Core.Repositories;

namespace DBWatcher.Core
{
    public interface IUnitOfWork
    {
        IMessageBroker Broker { get; }

        IScriptRepository ScriptRepository { get; }
        IConnectionPropertiesRepository ConnectionPropertiesRepository { get; }

        IJobRepository JobRepository { get; }

        IRepository<T, TKey> GetRepository<T, TKey>() where T : BaseDto<TKey>;
        IRepository<T, TKey> GetRepository<T, TKey>(string collectionName) where T : BaseDto<TKey>;
    }
}