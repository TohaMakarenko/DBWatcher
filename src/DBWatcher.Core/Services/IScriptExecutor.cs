using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;

namespace DBWatcher.Core.Services
{
    public interface IScriptExecutor
    {
        ConnectionProperties ConnectionProperties { get; }
        string DatabaseName { get; }
        
        Task<ScriptResult> ExecuteScript(Script script);
        Task<ScriptResult> InstallScriptToDb(string text);
    }
}