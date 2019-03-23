using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Execution
{
    /// <summary>
    ///     Script executors builder
    /// </summary>
    public interface IScriptExecutorManager
    {
        /// <summary>
        ///     Build script executor for connection properties
        /// </summary>
        /// <param name="connectionProperties"></param>
        /// <returns></returns>
        IScriptExecutor GetExecutor(ConnectionProperties connectionProperties);
    }
}