using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Repositories
{
    public interface IConnectionPropertiesRepository : IEventRepository<ConnectionProperties, int>
    {
        Task<List<ConnectionProperties>> GetShortInfoList();
    }
}