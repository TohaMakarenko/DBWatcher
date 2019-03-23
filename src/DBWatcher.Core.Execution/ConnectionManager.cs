using System.Collections.Generic;
using DBWatcher.Core.Enums;
using DBWatcher.Core.Execution;

namespace DBWatcher.Core.ScriptExecutor
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly Dictionary<RDBMS, IConnectionBuilder> _builders = new Dictionary<RDBMS, IConnectionBuilder>();

        public IConnectionBuilder GetBuilder(RDBMS rdbms)
        {
            return _builders[rdbms];
        }

        public void AddBuilder(RDBMS rdbms, IConnectionBuilder builder)
        {
            _builders.Add(rdbms, builder);
        }
    }
}