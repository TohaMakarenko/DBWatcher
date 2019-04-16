using System;
using System.Data;

namespace DBWatcher.Core.Execution
{
    [Serializable]
    /// <summary>
    ///     Script parameter
    /// </summary>
    public class Parameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DbType Type { get; set; }
    }
}