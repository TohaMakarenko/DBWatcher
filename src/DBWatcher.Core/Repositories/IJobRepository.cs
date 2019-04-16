using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Repositories
{
    public interface IJobRepository : IEventRepository<Job, int> { }
}