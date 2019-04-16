using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Enums;
using DBWatcher.Core.Exceptions;
using DBWatcher.Core.Scheduling;
using Quartz;

namespace DBWatcher.Scheduling.Quartz
{
    public class QuartzScriptScheduler : IScriptScheduler
    {
        private const string NameTemplate = "scriptJob_{0}";
        private const string Group = "script";

        public QuartzScriptScheduler(IUnitOfWork unitOfWork, IScheduler quartzScheduler)
        {
            UnitOfWork = unitOfWork;
            QuartzScheduler = quartzScheduler;
            UnitOfWork = unitOfWork;
            QuartzScheduler = quartzScheduler;
        }

        public IUnitOfWork UnitOfWork { get; }
        public IScheduler QuartzScheduler { get; }

        public async Task<int> CreateJob(Job job)
        {
            job = await UnitOfWork.GetRepository<Job, int>().Insert(job);
            return job.Id;
        }

        public async Task StartJob(int jobId)
        {
            var repo = UnitOfWork.JobRepository;
            var job = await repo.Get(jobId);
            if (job == null)
                throw new EntityNotFoundException<Job, int>(jobId);

            var jobDetails = BuildJob(job);
            var trigger = BuildTrigger(job);
            await QuartzScheduler.ScheduleJob(jobDetails, trigger);
            await repo.Update(job);
        }

        public Task StopJob(int jobId)
        {
            return QuartzScheduler.DeleteJob(GetJobKey(jobId));
        }

        /// <summary>
        ///     Build IJobDetail using Job data
        /// </summary>
        private IJobDetail BuildJob(Job job)
        {
            var jobData = new JobDataMap((IDictionary<string, object>)
                new Dictionary<string, object> {
                    ["Job"] = job
                });
            var jobBuilder = JobBuilder.Create<ScriptJob>()
                .WithIdentity(GetJobKey(job.Id))
                .SetJobData(jobData)
                .StoreDurably();
            return jobBuilder.Build();
        }

        /// <summary>
        ///     Build ITrigger using Job data
        /// </summary>
        private ITrigger BuildTrigger(Job job)
        {
            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(GetTriggerKey(job.Id));
            switch (job.Type) {
                case JobType.Cron:
                    triggerBuilder
                        .WithCronSchedule(job.Cron);
                    break;
                case JobType.Simple when job.IsRepeatable:
                    triggerBuilder.WithSimpleSchedule(x => x.WithInterval(job.Interval)
                        .RepeatForever());
                    break;
                case JobType.Simple:
                    triggerBuilder.WithSimpleSchedule();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (job.StartAt.HasValue)
                triggerBuilder.StartAt(job.StartAt.Value);
            else
                triggerBuilder.StartNow();

            return triggerBuilder.Build();
        }

        private JobKey GetJobKey(int jobId)
        {
            return new JobKey(string.Format(NameTemplate, jobId), Group);
        }

        private TriggerKey GetTriggerKey(int jobId)
        {
            return new TriggerKey(string.Format(NameTemplate, jobId), Group);
        }
    }
}