using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;

namespace DBWatcher.Core.Services
{
    public interface IScriptExecutor
    {
        ConnectionProperty ConnectionProperty { get; }
        string DatabaseName { get; }
        
        Task<IEnumerable<IEnumerable<dynamic>>> ExecuteScript(Script script);
        Task InstallScriptToDb(string text);
        Task<bool> IsScriptInstalled(Script script);
    }
}