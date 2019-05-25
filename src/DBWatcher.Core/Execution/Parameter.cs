using System;
using System.Data;

namespace DBWatcher.Core.Execution
{
    /// <summary>
    ///     Script parameter
    /// </summary>
    [Serializable]
    public class Parameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DbType Type { get; set; }
    }
}