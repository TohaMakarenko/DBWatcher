using System.Collections.Generic;

namespace DBWatcher.API.DTO
{
    public class FolderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> Scripts { get; set; }
    }
}