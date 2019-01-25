using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Repositories
{
    public interface IConnectionPropertyRepository
    {
        Task<ConnectionProperty> GetById(Guid id);
        Task<List<ConnectionProperty>> GetShortInfoList();
    }
}