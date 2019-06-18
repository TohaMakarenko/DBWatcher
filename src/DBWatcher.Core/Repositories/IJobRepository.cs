using DBWatcher.Core.Dto;
using DBWatcher.Core.Dto.Jobs;

namespace DBWatcher.Core.Repositories
{
    public interface IJobRepository : IEventRepository<Job, int> { }
}