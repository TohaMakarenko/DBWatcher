using DBWatcher.Core;
using DBWatcher.Core.Queue;
using Microsoft.Extensions.DependencyInjection;

namespace DBWatcher.Infrastructure.Data
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, string connectionString)
        {
            return services.AddSingleton<IUnitOfWork>(provider => {
                var bus = provider.GetService<IMessageBus>();
                return new UnitOfWork(connectionString, bus);
            });
        }
    }
}