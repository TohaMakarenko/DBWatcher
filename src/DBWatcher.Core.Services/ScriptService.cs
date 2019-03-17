using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.ScriptExecutor;

namespace DBWatcher.Core.Services
{
    public class ScriptService : IScriptService
    {
        private readonly IConnectionPropertiesService _connectionPropertiesService;

        public ScriptService(IConnectionPropertiesService connectionPropertiesService)
        {
            _connectionPropertiesService = connectionPropertiesService;
        }

        public IScriptExecutor GetScriptExecutor(ConnectionProperties connectionProperties, string databaseName = null)
        {
            return new ScriptExecutor(connectionProperties, databaseName);
        }

        public async Task<IScriptExecutor> GetScriptExecutor(int connectionPropertiesId, string databaseName = null)
        {
            return GetScriptExecutor(await _connectionPropertiesService.GetByIdDecrypted(connectionPropertiesId),
                databaseName);
        }
    }
}