using System.Collections.Generic;

namespace DBWatcher.API.DTO.Scripts
{
    public class ExecuteScriptDto
    {
        public int ConnectionPropsId { get; set; }
        public string Database { get; set; }
        public string Body { get; set; }
        public IEnumerable<ParameterDto> Params { get; set; }
    }
}