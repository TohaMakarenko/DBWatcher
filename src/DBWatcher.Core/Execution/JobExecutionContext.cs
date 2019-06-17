using System;

namespace DBWatcher.Core.Execution
{
    /// <summary>
    ///     Additional properties for script execution, define way of execution and connection
    /// </summary>
    [Serializable]
    public class JobExecutionContext
    {
        /// <summary>
        ///     Connection with which the request will be executed
        /// </summary>
        public int ConnectionId { get; set; }

        /// <summary>
        ///     Database name
        /// </summary>
        public string Database { get; set; }
    }
}