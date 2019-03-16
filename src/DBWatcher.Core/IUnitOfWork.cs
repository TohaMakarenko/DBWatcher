using DBWatcher.Core.Queue;
using DBWatcher.Core.Repositories;

namespace DBWatcher.Core
{
    public interface IUnitOfWork
    {
        IMessageBus Bus { get; }
        
        IScriptRepository ScriptRepository { get; }
        IConnectionPropertiesRepository ConnectionPropertiesRepository { get; }
    }
}