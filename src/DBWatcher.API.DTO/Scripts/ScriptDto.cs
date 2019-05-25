using System.Collections.Generic;

namespace DBWatcher.API.DTO.Scripts
{
    public class ScriptDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        /// <summary>
        ///     Script parameters declaration and default values
        /// </summary>
        public IEnumerable<ParameterDto> Parameters { get; set; }
    }
}