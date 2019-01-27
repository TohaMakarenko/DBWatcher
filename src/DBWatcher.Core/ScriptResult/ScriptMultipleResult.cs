using System.Collections.Generic;
using System.Linq;

namespace DBWatcher.Core.ScriptResult
{
    public class ScriptMultipleResult : ScriptResult<IEnumerable<dynamic>>

    {
        public override int TotalCount {
            get => Data.Sum(d => d.Count());
        }
    }
}