using System.Data.SqlClient;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public static class ConnectionManager
    {
        private static IConnectionBuilder _connectionBuilder;

        public static IConnectionBuilder ConnectionBuilder {
            get => _connectionBuilder ?? (_connectionBuilder = GetDefaultConnectionBuilder());
            set => _connectionBuilder = value;
        }

        public static SqlConnection GetConnection(ConnectionProperties connectionProperties, string databaseName = null)
        {
            return ConnectionBuilder.BuildConnection(connectionProperties, databaseName);
        }

        public static IConnectionBuilder GetDefaultConnectionBuilder()
        {
            return new ConnectionBuilder();
        }
    }
}