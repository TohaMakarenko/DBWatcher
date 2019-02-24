using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;
using DBWatcher.Core.Results;
using DBWatcher.Core.ScriptExecutor;

namespace DBWatcher.Core.Services
{
    public class ScriptExecutor : BaseScriptExecutor, IScriptExecutor
    {
        public ScriptExecutor(ConnectionProperties connectionProperties, string databaseName) : base(
            connectionProperties, databaseName) { }

        public ScriptExecutor(ConnectionProperties connectionProperties, string databaseName,
            IConnectionBuilder connectionBuilder) : base(connectionProperties, databaseName, connectionBuilder) { }

        public Task<ScriptResult<dynamic>> ExecuteScript(string script, object param = null)
        {
            return ExecuteScript<dynamic>(script, param);
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
                result.SetErrors(e);
            }

            return result;
        }

        public async Task<ScriptMultipleResult> ExecuteScriptMultiple(string script, object param = null)
        {
            var result = new ScriptMultipleResult();
            var data = new List<IEnumerable<dynamic>>();
            try {
                using (var connection = BuildConnection()) {
                    var reader = await connection.QueryMultipleAsync(script, param);
                    while (!reader.IsConsumed) {
                        data.Add(reader.Read());
                    }

                    result.Data = data;
                }
            }
            catch (SqlException e) {
                result.SetErrors(e);
            }

            return result;
        }
    }
}