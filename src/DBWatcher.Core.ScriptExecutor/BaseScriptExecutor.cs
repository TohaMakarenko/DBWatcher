using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
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

        protected DynamicParameters ToDynamicParameters(IEnumerable<Parameter> param)
        {
            var result = new DynamicParameters();
            foreach (var p in param) {
                result.Add(p.Name, p.Value, p.Type);
            }

            return result;
        }
    }
}