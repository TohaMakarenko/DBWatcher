using System.Data.SqlClient;
using System.Linq.Expressions;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public class ConnectionBuilder : IConnectionBuilder
    {
        public SqlConnection BuildConnection(ConnectionProperties connectionProperties, string databaseName = null)
        {
            return new SqlConnection(CreateConnectionString(connectionProperties.Server, databaseName,
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