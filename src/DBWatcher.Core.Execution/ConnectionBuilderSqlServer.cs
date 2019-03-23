using System.Data.SqlClient;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Execution
{
    public class ConnectionBuilderSqlServer : IConnectionBuilder
    {
        public SqlConnection BuildConnection(ConnectionProperties connectionProperties)
        {
            return new SqlConnection(CreateConnectionString(connectionProperties.Server, connectionProperties.Database,
                connectionProperties.Login, connectionProperties.Password));
        }

        private string CreateConnectionString(string server, string databaseName, string userId, string password)
        {
            var initialCatalog = "";
            if (!string.IsNullOrEmpty(databaseName))
                initialCatalog += $"Initial Catalog={databaseName};";
            var connectionString = $"Data Source={server}; {initialCatalog} User Id={userId}; Password={password}";
            return connectionString;
        }
    }
}