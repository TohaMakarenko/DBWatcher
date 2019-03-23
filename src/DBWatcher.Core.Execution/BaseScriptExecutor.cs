using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Execution
{
    public abstract class BaseScriptExecutor
    {
        protected readonly IConnectionBuilder ConnectionBuilder;

        public BaseScriptExecutor(ConnectionProperties connectionProperties, IConnectionBuilder connectionBuilder)
        {
            ConnectionProperties = connectionProperties;
            ConnectionBuilder = connectionBuilder;
        }

        public ConnectionProperties ConnectionProperties { get; }

        protected SqlConnection BuildConnection()
        {
            return ConnectionBuilder.BuildConnection(ConnectionProperties);
        }

        protected DynamicParameters ToDynamicParameters(IEnumerable<Parameter> param)
        {
            var result = new DynamicParameters();
            foreach (var p in param) result.Add(p.Name, p.Value, p.Type);

            return result;
        }
    }
}