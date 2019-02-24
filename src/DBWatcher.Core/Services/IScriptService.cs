using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.ScriptExecutor;

namespace DBWatcher.Core.Services
{
    public interface IScriptService
    {
        IScriptExecutor GetScriptExecutor(ConnectionProperties connectionProperties, string databaseName = null);
        Task<IScriptExecutor> GetScriptExecutor(Guid connectionPropertiesId, string databaseName = null);
    }
}