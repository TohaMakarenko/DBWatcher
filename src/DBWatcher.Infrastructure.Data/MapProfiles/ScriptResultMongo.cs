using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DBWatcher.Infrastructure.Data.MapProfiles
{
    public class ScriptResultMongo
    {
        public string Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public SqlException SqlException { get; set; }
        public IEnumerable<SqlError> Errors { get; set; }
        public virtual int TotalCount { get; set; }
    }
}