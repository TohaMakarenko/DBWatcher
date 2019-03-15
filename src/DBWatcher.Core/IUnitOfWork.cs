using DBWatcher.Core.Queue;
using DBWatcher.Core.Repositories;

namespace DBWatcher.Core
{
    public interface IUnitOfWork
    {
        IScriptRepository ScriptRepository { get; }
        IConnectionPropertiesRepository ConnectionPropertiesRepository { get; }
    }
}