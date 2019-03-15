namespace DBWatcher.Core.Events
{
    public interface IEventHandler<T, TKey>
    {
        void HandleInsert(T entity);
        void HandleUpdate(T entity);
        void HandleDelete(TKey id);
    }
}