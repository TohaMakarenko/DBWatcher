using System;
using System.Data;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Results;

namespace DBWatcher.Core.ScriptExecutor
{
    // TODO: implement it when needed
    public class ScriptExecutorTransactional : BaseScriptExecutor, IScriptExecutorTransactional
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public IDbConnection DbConnection { get; }

        public ScriptExecutorTransactional(ConnectionProperties connectionProperties, string databaseName) : base(
            connectionProperties, databaseName)
        {
            _connection = BuildConnection();
        }

        public ScriptExecutorTransactional(ConnectionProperties connectionProperties, string databaseName,
            IConnectionBuilder connectionBuilder) : base(connectionProperties, databaseName, connectionBuilder)
        {
            _connection = BuildConnection();
        }

        private void EnsureTransaction()
        {
            if(_disposed)
                throw new ObjectDisposedException(GetType().FullName);
            if (_transaction == null)
                _transaction = _connection.BeginTransaction();
        }

        public Task<ScriptResult<dynamic>> ExecuteScript(string script, object param = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScriptResult<T>> ExecuteScript<T>(string script, object param = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScriptMultipleResult> ExecuteScriptMultiple(string script, object param = null)
        {
            throw new System.NotImplementedException();
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _transaction = _connection.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed) {
                if (disposing) {
                    if (_transaction != null) {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_connection != null) {
                        _connection.Dispose();
                        _connection = null;
                    }
                }

                _disposed = true;
            }
        }
    }
}