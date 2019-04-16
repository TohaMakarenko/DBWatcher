using System;
using DBWatcher.Core.Results;

namespace DBWatcher.Core.Dto
{
    public class JobLog
    {
        public int JobId { get; set; }
        public DateTime Time { get; set; }
        public ScriptMultipleResult Result { get; set; }
    }
}