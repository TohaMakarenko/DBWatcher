using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DBWatcher.Core.Entities;
using DBWatcher.Core.ScriptExecutor;

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