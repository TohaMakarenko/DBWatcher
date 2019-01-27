using System.Data.SqlClient;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public interface IConnectionBuilder
    {
        SqlConnection BuildConnection(ConnectionProperties connectionProperties, string databaseName);
    }
}