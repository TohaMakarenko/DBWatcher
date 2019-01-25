using System;

namespace DBWatcher.Core.Entities
{
    public class ConnectionProperty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsPasswordEncrypted { get; set; }
    }
}