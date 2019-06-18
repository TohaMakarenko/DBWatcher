using System.Threading.Tasks;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Dto.Jobs;

namespace DBWatcher.Core.Scheduling
{
    public interface IScriptScheduler
    {
        /// <summary>
        ///     Create job in db
        /// </summary>
        Task<int> CreateJob(Job job);

        /// <summary>
        ///     Start job by id
        /// </summary>
        Task StartJob(int jobId);

        /// <summary>
        ///     Stop job by id
        /// </summary>
        Task StopJob(int jobId);
    }
}