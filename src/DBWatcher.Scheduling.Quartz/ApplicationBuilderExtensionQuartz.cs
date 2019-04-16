using System;
using Microsoft.AspNetCore.Builder;
using Quartz;
using Quartz.Spi;

namespace DBWatcher.Scheduling.Quartz
{
    public static class ApplicationBuilderExtensionQuartz
    {
        public static IApplicationBuilder UseQuartz(this IApplicationBuilder builder, IServiceProvider container)
        {
            var factory = (IJobFactory) container.GetService(typeof(IJobFactory));
            var schedulerFactory = (ISchedulerFactory) container.GetService(typeof(ISchedulerFactory));
            var scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.JobFactory = factory;
            return builder;
        }
    }
}