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

        public BaseScriptExecutor(ConnectionProperties connectionProperties, ExecutionContext context, IConnectionBuilder connectionBuilder)
        {
            ConnectionProperties = connectionProperties;
            Context = context;
            ConnectionBuilder = connectionBuilder;
            if (!string.IsNullOrEmpty(context.Database))
                ConnectionProperties.WithDatabaseName(context.Database); // todo it will change conProp object, be careful
        }

        public ConnectionProperties ConnectionProperties { get; }
        public ExecutionContext Context { get; }

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