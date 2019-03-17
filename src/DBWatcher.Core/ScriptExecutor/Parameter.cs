using System.Data;

namespace DBWatcher.Core.ScriptExecutor
{
    public class Parameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DbType Type { get; set; }
    }
}