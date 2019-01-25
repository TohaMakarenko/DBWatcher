using System;

namespace DBWatcher.Core.Entities
{
    public class Script
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public string DbObjectName { get; set; }
    }
}