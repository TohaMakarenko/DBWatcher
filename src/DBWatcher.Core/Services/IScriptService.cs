using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public interface IScriptService
    {
        IScriptExecutor GetScriptExecutor(ConnectionProperty connectionProperty, string databaseName);
    }
}