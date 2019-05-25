using System.Collections.Generic;

namespace DBWatcher.API.DTO.Scripts
{
    public class ExecuteScriptDto
    {
        public int ConnectionId { get; set; }
        public string Database { get; set; }
        public int ScriptId { get; set; }
        public IEnumerable<ParameterDto> Params { get; set; }
    }
}