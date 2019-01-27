using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public interface IScriptService
    {
        IScriptExecutor GetScriptExecutor(ConnectionProperties connectionProperties, string databaseName);
        Task<IScriptExecutor> GetScriptExecutor(Guid connectionPropertiesId, string databaseName);
    }
}