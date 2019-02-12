namespace DBWatcher.Core.Repositories
{
    public interface IUnitOfWork
    {
        IScriptRepository ScriptRepository { get; }
        IConnectionPropertiesRepository ConnectionPropertiesRepository { get; }
    }
}