using System.Collections.Generic;

namespace DBWatcher.Core.Dto
{
    /// <summary>
    ///     Directory containing scripts
    /// </summary>
    public class Folder : BaseDto<int>
    {
        public string Name { get; set; }
        public IEnumerable<int> Scripts { get; set; }
    }
}