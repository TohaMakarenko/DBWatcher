using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Results;

namespace DBWatcher.Core.ScriptExecutor
{
    public interface IScriptExecutor
    {
        ConnectionProperties ConnectionProperties { get; }
        string DatabaseName { get; }
        
        Task<ScriptResult<dynamic>> ExecuteScript(string script, object param = null);
        Task<ScriptResult<T>> ExecuteScript<T>(string script, object param = null);
        Task<ScriptMultipleResult> ExecuteScriptMultiple(string script, object param = null);
    }
}