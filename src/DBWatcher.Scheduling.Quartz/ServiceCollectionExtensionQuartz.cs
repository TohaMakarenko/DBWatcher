using System;
using System.Collections.Specialized;
using DBWatcher.Core.Scheduling;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Quartz.Spi.MongoDbJobStore;

namespace DBWatcher.Scheduling.Quartz
{
    public static class ServiceCollectionExtensionQuartz
    {
        public static IServiceCollection AddQuartz(this IServiceCollection services, QuartzProperties properties)
        {
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton(c => BuildScheduler(c, properties));

            return services;
        }

        public static IServiceCollection AddQuartzScriptScheduler(this IServiceCollection services)
        {
            services.AddSingleton<IScriptScheduler, QuartzScriptScheduler>();

            return services;
        }

        private static IScheduler BuildScheduler(IServiceProvider container, QuartzProperties properties)
        {
            var propsCollection = new NameValueCollection();
            propsCollection[StdSchedulerFactory.PropertySchedulerInstanceName] = properties.InstanceName;
            propsCollection[StdSchedulerFactory.PropertySchedulerInstanceId] =
                $"{Environment.MachineName}-{Guid.NewGuid()}";
            propsCollection[StdSchedulerFactory.PropertyJobStoreType] = typeof(MongoDbJobStore).AssemblyQualifiedName;
            propsCollection[
                    $"{StdSchedulerFactory.PropertyJobStorePrefix}.{StdSchedulerFactory.PropertyDataSourceConnectionString}"]
                = properties.StoreConnectionString;
            propsCollection["quartz.serializer.type"] = properties.SerializerType;

            var jobFactory = (IJobFactory) container.GetService(typeof(IJobFactory));
            var factory = new SchedulerFactory(propsCollection, jobFactory);
            var scheduler = factory.GetScheduler().Result;
            scheduler.JobFactory = jobFactory;
            scheduler.Start();
            return scheduler;
        }
    }
}