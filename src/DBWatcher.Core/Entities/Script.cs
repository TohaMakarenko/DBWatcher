using System;

namespace DBWatcher.Core.Entities
{
    public class Script: BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
    }
}