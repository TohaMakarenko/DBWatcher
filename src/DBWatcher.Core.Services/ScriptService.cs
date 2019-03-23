using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Execution;

namespace DBWatcher.Core.Services
{
    public class ScriptService : IScriptService
    {
        private readonly IConnectionPropertiesService _connectionPropertiesService;
        private readonly IScriptExecutorManager _scriptManager;

        public ScriptService(IConnectionPropertiesService connectionPropertiesService,
            IScriptExecutorManager scriptManager)
        {
            _connectionPropertiesService = connectionPropertiesService;
            _scriptManager = scriptManager;
        }

        public IScriptExecutor GetScriptExecutor(ConnectionProperties connectionProperties)
        {
            return _scriptManager.GetExecutor(connectionProperties);
        }

        public async Task<IScriptExecutor> GetScriptExecutor(int connectionPropertiesId, string databaseName = null)
        {
            var conProps = await _connectionPropertiesService.GetByIdDecrypted(connectionPropertiesId);
            conProps.Database = databaseName;
            return GetScriptExecutor(conProps);
        }
    }
}