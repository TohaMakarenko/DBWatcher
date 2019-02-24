namespace DBWatcher.API.DTO.Scripts
{
    public class ScriptFilterDto
    {
        public string Name { get; set; }
        public bool IncludeDescription { get; set; }
        public int? Offset { get; set; }
        public int? Count { get; set; }
    }
}