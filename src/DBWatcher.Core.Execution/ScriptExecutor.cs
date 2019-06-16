using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Results;

namespace DBWatcher.Core.Execution
{
    public class ScriptExecutor : BaseScriptExecutor, IScriptExecutor
    {
        public ScriptExecutor(ConnectionProperties connectionProperties, IConnectionBuilder connectionBuilder) : base(
            connectionProperties, connectionBuilder) { }

        public ScriptExecutor(ConnectionProperties connectionProperties, ExecutionContext context, IConnectionBuilder connectionBuilder) : base(
            connectionProperties, context, connectionBuilder) { }

        public Task<ScriptResult<dynamic>> ExecuteScript(string script, object param = null)
        {
            return ExecuteScript<dynamic>(script, param);
        }

        public Task<ScriptResult<dynamic>> ExecuteScript(string script, IEnumerable<Parameter> param)
        {
            var dynamicParams = ToDynamicParameters(param);
            return ExecuteScript(script, dynamicParams);
        }

        public async Task<ScriptResult<T>> ExecuteScript<T>(string script, object param = null)
        {
            var result = new ScriptResult<T>();
            try {
                using (var connection = BuildConnection()) {
                    result.Data = await connection.QueryAsync<T>(script, param);
                }
            }
            catch (SqlException e) {
                result.SetException(e);
            }

            return result;
        }

        public Task<ScriptResult<T>> ExecuteScript<T>(string script, IEnumerable<Parameter> param)
        {
            var dynamicParams = ToDynamicParameters(param);
            return ExecuteScript<T>(script, dynamicParams);
        }

        public async Task<ScriptMultipleResult> ExecuteScriptMultiple(string script, object param = null)
        {
            var result = new ScriptMultipleResult();
            var data = new List<IEnumerable<dynamic>>();
            try {
                using (var connection = BuildConnection()) {
                    var reader = await connection.QueryMultipleAsync(script, param);
                    while (!reader.IsConsumed)
                        data.Add(reader.Read());

                    result.Data = data;
                }
            }
            catch (SqlException e) {
                result.SetException(e);
            }

            return result;
        }

        public Task<ScriptMultipleResult> ExecuteScriptMultiple(string script, IEnumerable<Parameter> param)
        {
            var dynamicParams = ToDynamicParameters(param);
            return ExecuteScriptMultiple(script, dynamicParams);
        }
    }
}