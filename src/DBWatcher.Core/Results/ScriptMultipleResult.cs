using System.Collections.Generic;
using System.Linq;

namespace DBWatcher.Core.Results
{
    public class ScriptMultipleResult : ScriptResult<IEnumerable<dynamic>>

    {
        public override int TotalCount {
            get => Data.Sum(d => d.Count());
        }
    }
}