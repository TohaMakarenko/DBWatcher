using System;

namespace DBWatcher.API.DTO.Scripts
{
    public class ScriptDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
    }
}