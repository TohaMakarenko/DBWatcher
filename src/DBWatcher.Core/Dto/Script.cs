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
    }
}