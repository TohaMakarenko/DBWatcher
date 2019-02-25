namespace DBWatcher.Core.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}