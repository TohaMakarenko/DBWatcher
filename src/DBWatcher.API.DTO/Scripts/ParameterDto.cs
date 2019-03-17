using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DBWatcher.API.DTO.Scripts
{
    public class ParameterDto
    {
        public string Name { get; set; }
        public object Value { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DbType Type { get; set; }
    }
}