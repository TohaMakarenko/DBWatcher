namespace DBWatcher.API.DTO.Scripts
{
    public class ConnectionPropertiesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsPasswordEncrypted { get; set; }
    }
}