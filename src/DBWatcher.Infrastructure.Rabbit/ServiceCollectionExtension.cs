using DBWatcher.Core.Queue;
using Microsoft.Extensions.DependencyInjection;

namespace DBWatcher.Infrastructure.Rabbit
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRabbitMessageBus(this IServiceCollection services, string connectionString)
        {
            return services.AddSingleton<IMessageBroker>(provider => new MessageBroker(connectionString));
        }
    }
}