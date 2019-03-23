using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Repositories
{
    public interface IScriptRepository : IEventRepository<Script, int>
    {
        Task<List<Script>> GetShortInfoPage(int offset, int count);
        Task<List<Script>> FindByName(string name);
        Task<List<Script>> FindByNameOrDescription(string name, string description);
    }
}