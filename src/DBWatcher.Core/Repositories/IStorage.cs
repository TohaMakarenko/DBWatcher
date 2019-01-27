namespace DBWatcher.Core.Repositories
{
    public interface IStorage
    {
        IScriptRepository ScriptRepository { get; }
        IConnectionPropertiesRepository ConnectionPropertiesRepository { get; }
    }
}