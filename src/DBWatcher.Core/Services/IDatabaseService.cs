using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public interface IDatabaseService
    {
        Task<IEnumerable<string>> GetDatabases(ConnectionProperties connectionProperties);
        Task<bool> IsObjectExists(ConnectionProperties connectionProperties, string name);
    }
}