using System;
using AutoMapper;
using DBWatcher.Core;
using DBWatcher.Core.Queue;
using Microsoft.Extensions.DependencyInjection;

namespace DBWatcher.Infrastructure.Data
{
    public static class ServiceCollectionExtensionData
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, string connectionString,
            params Func<IUnitOfWork, IServiceProvider, IUnitOfWork>[] configs)
        {
            return services.AddSingleton(provider => {
                var bus = provider.GetService<IMessageBroker>();
                var mapper = provider.GetService<IMapper>();
                IUnitOfWork work = new UnitOfWork(connectionString, bus, mapper);
                if (configs.Length > 0)
                    foreach (var config in configs)
                        work = config(work, provider);

                return work;
            });
        }
    }
}