using System;
using System.Threading.Tasks;

namespace DBWatcher.Core.Queue
{
    public interface IMessageBroker
    {
        Task PublishAsync<T>(T message) where T : class;
        void SubscribeAsync<T>(string subscriptionId, Func<T, Task> handler) where T : class;
    }
}