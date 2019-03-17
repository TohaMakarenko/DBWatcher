using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public interface IConnectionPropertiesService
    {
        Task SaveConnectionProperty(ConnectionProperties connectionProperties);
        Task<ConnectionProperties> GetById(int id);
        Task<ConnectionProperties> GetByIdDecrypted(int id);
    }
}