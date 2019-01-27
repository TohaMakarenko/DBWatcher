using System.Collections.Generic;
using System.Linq;

namespace DBWatcher.Core.ScriptResult
{
    public class ScriptResult<TResult> : ScriptResult
    {
        public IEnumerable<TResult> Data { get; set; }
        
        public virtual int TotalCount {
            get => Data.Count();
        }
    }
}