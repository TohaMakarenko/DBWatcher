using System;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public interface IConnectionPropertiesService
    {
        Task SaveConnectionProperty(ConnectionProperties connectionProperties);
        Task<ConnectionProperties> GetById(Guid id);
        Task<ConnectionProperties> GetByIdDecrypted(Guid id);
    }
}