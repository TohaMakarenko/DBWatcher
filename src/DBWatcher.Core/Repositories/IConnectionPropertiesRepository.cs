using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Repositories
{
    public interface IConnectionPropertiesRepository : IEventRepository<ConnectionProperties, int>
    {
        Task<List<ConnectionProperties>> GetShortInfoList();
    }
}