using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Repositories
{
    public interface IJobLogRepository : IEventRepository<JobLog, string> { }
}