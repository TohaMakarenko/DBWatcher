using System;
using System.Collections.Generic;
using DBWatcher.Core.Enums;
using DBWatcher.Core.Execution;

namespace DBWatcher.Core.Dto.Jobs
{
    /// <summary>
    ///     Job
    /// </summary>
    [Serializable]
    public class Job : BaseDto<int>
    {
        public string Name { get; set; }

        /// <summary>
        ///     Scrip to be executed
        /// </summary>
        public int ScriptId { get; set; }

        /// <summary>
        ///     Additional properties for script execution
        /// </summary>
        public IEnumerable<JobExecutionContext> ExecutionContexts { get; set; }

        /// <summary>
        ///     Script parameters
        /// </summary>
        public IEnumerable<Parameter> Parameters { get; set; }

        /// <summary>
        ///     Job type
        /// </summary>
        public JobType Type { get; set; }

        /// <summary>
        ///     Corn string
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        ///     Start job at
        /// </summary>
        public DateTime? StartAt { get; set; }

        /// <summary>
        ///     Repeat interval
        /// </summary>
        public TimeSpan? Interval { get; set; }

        /// <summary>
        ///     IsRepeatable
        /// </summary>
        public bool IsRepeatable { get; set; }

        /// <summary>
        ///     IsActive
        /// </summary>
        public bool IsActive { get; set; }
    }
}