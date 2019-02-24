using System.Data;

namespace DBWatcher.Core.ScriptExecutor
{
    public interface IScriptExecutorTransactional : IScriptExecutorConnected
    {
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        void CommitTransaction();
        void RollbackTransaction();
    }
}