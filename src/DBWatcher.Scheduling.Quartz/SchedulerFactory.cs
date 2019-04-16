using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace DBWatcher.Scheduling.Quartz
{
    public class SchedulerFactory : StdSchedulerFactory
    {
        public SchedulerFactory(IJobFactory jobFactory)
        {
            JobFactory = jobFactory;
        }

        public SchedulerFactory(NameValueCollection props, IJobFactory jobFactory) : base(props)
        {
            JobFactory = jobFactory;
        }

        public IJobFactory JobFactory { get; }

        public override async Task<IScheduler> GetScheduler(
            CancellationToken cancellationToken = new CancellationToken())
        {
            var sch = await base.GetScheduler(cancellationToken).ConfigureAwait(false);
            sch.JobFactory = JobFactory;
            return sch;
        }

        public override async Task<IScheduler> GetScheduler(string schedName,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var sch = await base.GetScheduler(schedName, cancellationToken).ConfigureAwait(false);
            sch.JobFactory = JobFactory;
            return sch;
        }
    }
}