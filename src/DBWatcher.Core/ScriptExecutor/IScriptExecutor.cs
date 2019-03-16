using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Results;

namespace DBWatcher.Core.ScriptExecutor
{
    /// <summary>
    /// SQL scripts executor
    /// </summary>
    public interface IScriptExecutor
    {
        ConnectionProperties ConnectionProperties { get; }
        string DatabaseName { get; }

        /// <summary>
        /// Execute script using parameters object
        /// </summary>
        /// <param name="script">script to execute</param>
        /// <param name="param">used parameters</param>
        /// <returns>dynamic script result</returns>
        Task<ScriptResult<dynamic>> ExecuteScript(string script, object param = null);

        /// <summary>
        /// Execute script using parameters object
        /// </summary>
        /// <param name="script">script to execute</param>
        /// <param name="param">used parameters</param>
        /// <typeparam name="T">returned result type</typeparam>
        /// <returns>strong typed script result</returns>
        Task<ScriptResult<T>> ExecuteScript<T>(string script, object param = null);

        /// <summary>
        /// Execute script using parameters object
        /// </summary>
        /// <param name="script">script to execute</param>
        /// <param name="param">used parameters</param>
        /// <returns>multiple results set</returns>
        Task<ScriptMultipleResult> ExecuteScriptMultiple(string script, object param = null);
    }
}