using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Repositories
{
    public interface IConnectionPropertiesRepository
    {
        Task<ConnectionProperties> GetById(Guid id);
        Task<List<ConnectionProperties>> GetShortInfoList();
        Task InsertConnection(ConnectionProperties connectionProperties);
        Task UpdateConnection(ConnectionProperties connectionProperties);
    }
}