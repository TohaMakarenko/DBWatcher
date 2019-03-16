using System;

namespace DBWatcher.Core.Entities
{
    /// <summary>
    /// Properties for connecting to server
    /// </summary>
    public class ConnectionProperties : BaseEntity<Guid>
    {
        /// <summary>
        /// Connection name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// RDBMS id
        /// </summary>
        public RDBMS System { get; set; }
        /// <summary>
        /// Server address
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Is password encrypted
        /// </summary>
        public bool IsPasswordEncrypted { get; set; }
    }
}