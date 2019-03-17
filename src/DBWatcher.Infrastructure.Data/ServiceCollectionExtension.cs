using System;
using DBWatcher.Core;
using DBWatcher.Core.Queue;
using Microsoft.Extensions.DependencyInjection;

namespace DBWatcher.Infrastructure.Data
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, string connectionString,
            params Func<IUnitOfWork, IServiceProvider, IUnitOfWork>[] configs)
        {
            return services.AddSingleton<IUnitOfWork>(provider => {
                var bus = provider.GetService<IMessageBus>();
                IUnitOfWork work = new UnitOfWork(connectionString, bus);
                if (configs.Length > 0)
                    foreach (var config in configs) {
                        work = config(work, provider);
                    }

                return work;
            });
        }
    }
}