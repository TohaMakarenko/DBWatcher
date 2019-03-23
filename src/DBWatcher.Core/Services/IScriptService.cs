using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Execution;

namespace DBWatcher.Core.Services
{
    /// <summary>
    ///     ScriptExecutor manager
    /// </summary>
    public interface IScriptService
    {
        /// <summary>
        ///     Return script executor for connection properties
        /// </summary>
        /// <param name="connectionProperties"></param>
        /// <returns></returns>
        IScriptExecutor GetScriptExecutor(ConnectionProperties connectionProperties);

        /// <summary>
        /// </summary>
        /// <param name="connectionPropertiesId"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        Task<IScriptExecutor> GetScriptExecutor(int connectionPropertiesId, string databaseName = null);
    }
}