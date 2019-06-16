using System;
using DBWatcher.Infrastructure.Data.MapProfiles;

namespace DBWatcher.Infrastructure.Data.Entities
{
    public class JobLogMongo
    {
        public string Id { get; set; }
        public int JobId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public ScriptResultMongo Result { get; set; }
    }
}