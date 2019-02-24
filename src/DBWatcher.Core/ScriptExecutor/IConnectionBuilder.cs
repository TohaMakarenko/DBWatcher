using System.Data.SqlClient;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.ScriptExecutor
{
    public interface IConnectionBuilder
    {
        SqlConnection BuildConnection(ConnectionProperties connectionProperties, string databaseName = null);
    }
}