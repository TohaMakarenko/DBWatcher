using System.Threading.Tasks;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Repositories
{
    public interface IFolderRepository : IRepository<Folder, int>
    {
        Task<Folder> AddScript(int id, int scriptId);
        Task<Folder> RemoveScript(int id, int scriptId);
    }
}