using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public class DatabaseService : IDatabaseService
    {
        public Task<IEnumerable<string>> GetDatabases(ConnectionProperties connectionProperties)
        {
            using (var connection = ConnectionManager.GetConnection(connectionProperties)) {
                return connection.QueryAsync<string>("select name from sys.databases");
            }
        }

        public async Task<bool> IsObjectExists(ConnectionProperties connectionProperties, string name)
        {
            using (var connection = ConnectionManager.GetConnection(connectionProperties)) {
                return await connection.QueryFirstOrDefaultAsync<int?>("select OBJECT_ID(@objectName)",
                           new {objectName = name}) == null;
            }
        }
    }
}