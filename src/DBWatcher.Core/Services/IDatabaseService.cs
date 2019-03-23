using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Services
{
    public interface IDatabaseService
    {
        Task<IEnumerable<string>> GetDatabases(ConnectionProperties connectionProperties);
    }
}