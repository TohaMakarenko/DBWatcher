namespace DBWatcher.Core.Dto
{
    // todo Entities will be stored separately in data infrastructure project, cuz we don't need them in core
    public class BaseDto<TKey>
    {
        public TKey Id { get; set; }
    }
}