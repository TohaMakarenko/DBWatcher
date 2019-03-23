using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Execution
{
    public class ScriptExecutorManager : IScriptExecutorManager
    {
        private readonly IConnectionManager _connectionManager;

        public ScriptExecutorManager(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public IScriptExecutor GetExecutor(ConnectionProperties connectionProperties)
        {
            var builder = _connectionManager.GetBuilder(connectionProperties.System);
            return new ScriptExecutor(connectionProperties, builder);
        }
    }
}