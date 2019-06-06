using System;
using DBWatcher.Core.Results;

namespace DBWatcher.Core.Dto
{
    public class JobLog : BaseDto<string>
    {
        public int JobId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public ScriptMultipleResult Result { get; set; }
    }
}