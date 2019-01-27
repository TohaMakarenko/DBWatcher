using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public interface IDatabaseService
    {
        Task<string> GetDatabases(ConnectionProperties connectionProperties);
        Task<bool> IsObjectExists(string name);
    }
}