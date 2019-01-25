using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public interface IConnectionService
    {
        Task CreateConnection(ConnectionProperty connectionProperty, bool needSavePassword, bool needEncryptPassword);
    }
}