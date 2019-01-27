using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;

namespace DBWatcher.Core.Services
{
    public class ScriptExecutor : IScriptExecutor
    {
        private readonly IConnectionBuilder _connectionBuilder;
        public ConnectionProperties ConnectionProperties { get; }
        public string DatabaseName { get; }

        public ScriptExecutor(ConnectionProperties connectionProperties, string databaseName)
        {
            _connectionBuilder = ConnectionManager.ConnectionBuilder;
            ConnectionProperties = connectionProperties;
            DatabaseName = databaseName;
        }

        public ScriptExecutor(ConnectionProperties connectionProperties, string databaseName,
            IConnectionBuilder connectionBuilder)
            : this(connectionProperties, databaseName)
        {
            _connectionBuilder = connectionBuilder;
        }

        private SqlConnection BuildConnection()
        {
            return _connectionBuilder.BuildConnection(ConnectionProperties, DatabaseName);
        }

        public async Task<ScriptResult> ExecuteScript(Script script)
        {
            var result = new ScriptResult();
            var data = new List<IEnumerable<dynamic>>();
            try {
                using (var connection = BuildConnection()) {
                    var reader = await connection.QueryMultipleAsync(script.Body);
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

        public async Task<ScriptResult> InstallScriptToDb(string text)
        {
            var result = new ScriptResult();
            try {
                using (var connection = BuildConnection()) {
                    await connection.QueryAsync(text);
                }
            }
            catch (SqlException e) {
                result.SetErrors(e);
            }

            return result;
        }
    }
}