using System;
using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.ScriptExecutor;

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
        /// <param name="databaseName"></param>
        /// <returns></returns>
        IScriptExecutor GetScriptExecutor(ConnectionProperties connectionProperties, string databaseName = null);

        /// <summary>
        /// </summary>
        /// <param name="connectionPropertiesId"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        Task<IScriptExecutor> GetScriptExecutor(Guid connectionPropertiesId, string databaseName = null);
    }
}