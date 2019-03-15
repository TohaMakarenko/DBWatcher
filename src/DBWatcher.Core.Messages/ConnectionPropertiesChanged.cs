using System;

namespace DBWatcher.Core.Messages
{
    public class ConnectionPropertiesChanged
    {
        public Guid Id { get; set; }
        public string Server { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsPasswordEncrypted { get; set; }
    }
}