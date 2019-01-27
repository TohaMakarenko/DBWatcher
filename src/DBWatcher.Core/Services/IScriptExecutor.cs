using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;
using DBWatcher.Core.ScriptResult;

namespace DBWatcher.Core.Services
{
    public interface IScriptExecutor
    {
        ConnectionProperties ConnectionProperties { get; }
        string DatabaseName { get; }
        
        Task<ScriptMultipleResult> ExecuteScript(Script script);
        Task<ScriptResult.ScriptResult> InstallScriptToDb(string text);
    }
}