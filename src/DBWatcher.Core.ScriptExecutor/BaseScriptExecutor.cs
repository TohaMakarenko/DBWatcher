using System.Data.SqlClient;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.ScriptExecutor
{
    public abstract class BaseScriptExecutor
    {
        protected readonly IConnectionBuilder ConnectionBuilder;
        public ConnectionProperties ConnectionProperties { get; }
        public string DatabaseName { get; }
        
        protected BaseScriptExecutor(ConnectionProperties connectionProperties, string databaseName)
        {
            ConnectionBuilder = ConnectionManager.ConnectionBuilder;
            ConnectionProperties = connectionProperties;
            DatabaseName = databaseName;
        }

        protected BaseScriptExecutor(ConnectionProperties connectionProperties, string databaseName,
            IConnectionBuilder connectionBuilder)
            : this(connectionProperties, databaseName)
        {
            ConnectionBuilder = connectionBuilder;
        }

        protected SqlConnection BuildConnection()
        {
            return ConnectionBuilder.BuildConnection(ConnectionProperties, DatabaseName);
        }
    }
}