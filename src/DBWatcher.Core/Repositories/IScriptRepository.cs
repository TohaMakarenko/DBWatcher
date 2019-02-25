using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Repositories
{
    public interface IScriptRepository : IEventRepository<Script, Guid>
    {
        Task<List<Script>> GetShortInfoPage(int offset, int count);
        Task<List<Script>> FindByName(string name);
        Task<List<Script>> FindByNameOrDescription(string name, string description);
    }
}