using System;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace DBWatcher.Scheduling.Quartz
{
    public class JobFactory : PropertySettingJobFactory
    {
        protected readonly IServiceProvider Container;

        public JobFactory(IServiceProvider container)
        {
            Container = container;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var job = Container.GetService(bundle.JobDetail.JobType) as IJob;

            var jobDataMap = new JobDataMap();
            jobDataMap.PutAll(scheduler.Context);
            jobDataMap.PutAll(bundle.JobDetail.JobDataMap);
            jobDataMap.PutAll(bundle.Trigger.JobDataMap);

            SetObjectProperties(job, jobDataMap);

            return job;
        }
    }
}