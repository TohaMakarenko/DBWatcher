using System;

namespace DBWatcher.Core.Execution
{
    /// <summary>
    ///     Additional properties for script execution, define way of execution and connection
    /// </summary>
    [Serializable]
    public class ExecutionContext
    {
        /// <summary>
        ///     Database name
        /// </summary>
        public string Database { get; set; }
    }
}