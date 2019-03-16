using System.Data.SqlClient;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.ScriptExecutor
{
    /// <summary>
    /// Sql connection builder
    /// </summary>
    public interface IConnectionBuilder
    {
        /// <summary>
        /// Build sql connection for connectionProperties
        /// </summary>
        /// <param name="connectionProperties"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        SqlConnection BuildConnection(ConnectionProperties connectionProperties, string databaseName = null);
    }
}