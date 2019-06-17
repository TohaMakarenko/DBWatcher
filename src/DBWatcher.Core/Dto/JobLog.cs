using System;
using DBWatcher.Core.Execution;
using DBWatcher.Core.Results;

namespace DBWatcher.Core.Dto
{
    public class JobLog : BaseDto<string>
    {
        public int JobId { get; set; }
        public int ConnectionId { get; set; }
        public string Database { get; set; }
        public JobExecutionContext Context { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public ScriptMultipleResult Result { get; set; }
    }
}