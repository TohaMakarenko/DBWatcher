using System;
using System.Data;

namespace DBWatcher.Core.ScriptExecutor
{
    public interface IScriptExecutorConnected : IScriptExecutor, IDisposable
    {
        IDbConnection DbConnection { get;}
    }
}