using System.Collections.Generic;
using DBWatcher.API.DTO.Scripts;

namespace DBWatcher.API.DTO
{
    public class FolderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ScriptInfoDto> Scripts { get; set; }
    }
}