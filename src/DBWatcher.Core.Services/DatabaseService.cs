using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IScriptService _scriptService;

        public DatabaseService(IScriptService scriptService)
        {
            _scriptService = scriptService;
        }

        public async Task<IEnumerable<string>> GetDatabases(ConnectionProperties connectionProperties)
        {
            var result = await _scriptService.GetScriptExecutor(connectionProperties)
                .ExecuteScript<string>("select name from sys.databases");
            return result.GetResult();
        }
    }
}