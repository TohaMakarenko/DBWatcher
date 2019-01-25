namespace DBWatcher.Core.Repositories
{
    public interface IStorage
    {
        IScriptRepository ScriptRepository { get; }
        IConnectionPropertyRepository ConnectionPropertyRepository { get; }
    }
}