using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core.Dto;

namespace DBWatcher.Core.Repositories
{
    public interface IJobLogRepository
    {
        Task<JobLog> Insert(JobLog entity);
        Task<IEnumerable<JobLog>> GetForJob(int jobId);
        Task<IEnumerable<JobLog>> GetForJob(int jobId, int skip, int take);
    }
}