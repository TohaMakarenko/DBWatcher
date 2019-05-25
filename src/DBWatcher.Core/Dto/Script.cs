using System.Collections.Generic;
using DBWatcher.Core.Execution;

namespace DBWatcher.Core.Dto
{
    /// <summary>
    ///     SQL script details
    /// </summary>
    public class Script : BaseDto<int>
    {
        /// <summary>
        ///     Script name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Script author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     Script text
        /// </summary>
        public string Body { get; set; }
        
        /// <summary>
        ///     Script parameters declaration and default values
        /// </summary>
        public IEnumerable<Parameter> Parameters { get; set; }
    }
}