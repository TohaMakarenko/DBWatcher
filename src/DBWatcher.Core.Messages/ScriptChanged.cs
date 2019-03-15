using System;

namespace DBWatcher.Core.Messages
{
    public class ScriptChanged
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
    }
}