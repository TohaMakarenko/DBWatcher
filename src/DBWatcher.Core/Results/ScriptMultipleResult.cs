using System.Collections.Generic;
using System.Linq;

namespace DBWatcher.Core.Results
{
    /// <summary>
    ///     Represents multiple result sets
    /// </summary>
    public class ScriptMultipleResult : ScriptResult<IEnumerable<dynamic>>
    {
        public override int TotalCount => Data?.Sum(d => d.Count()) ?? 0;
    }
}