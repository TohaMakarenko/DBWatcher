using DBWatcher.Core.Enums;

namespace DBWatcher.Core.Execution
{
    /// <summary>
    ///     Contains methods for building and storing connection builders
    /// </summary>
    public interface IConnectionManager
    {
        /// <summary>
        ///     Get connection builder for RDBMS
        /// </summary>
        /// <param name="rdbms"></param>
        /// <returns></returns>
        IConnectionBuilder GetBuilder(RDBMS rdbms);

        /// <summary>
        ///     Add connection builder for RDBMS
        /// </summary>
        /// <param name="rdbms"></param>
        /// <param name="builder"></param>
        void AddBuilder(RDBMS rdbms, IConnectionBuilder builder);
    }
}