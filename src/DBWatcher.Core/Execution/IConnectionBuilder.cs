using System.Data.SqlClient;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Execution
{
    /// <summary>
    ///     Sql connection builder
    /// </summary>
    public interface IConnectionBuilder
    {
        /// <summary>
        ///     Build sql connection for connectionProperties
        /// </summary>
        /// <returns></returns>
        SqlConnection BuildConnection(ConnectionProperties connectionProperties);
    }
}