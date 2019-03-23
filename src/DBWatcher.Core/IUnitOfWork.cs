using DBWatcher.Core.Queue;
using DBWatcher.Core.Repositories;

namespace DBWatcher.Core
{
    public interface IUnitOfWork
    {
        IMessageBroker Broker { get; }

        IScriptRepository ScriptRepository { get; }
        IConnectionPropertiesRepository ConnectionPropertiesRepository { get; }
    }
}