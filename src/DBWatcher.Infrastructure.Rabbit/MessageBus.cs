using System;
using System.Threading.Tasks;
using DBWatcher.Core.Queue;
using EasyNetQ;

namespace DBWatcher.Infrastructure.Rabbit
{
    public class MessageBus : IMessageBus
    {
        public IBus Bus { get; set; }

        public MessageBus(string connectionString)
        {
            Bus = RabbitHutch.CreateBus(connectionString);
        }
        
        public Task PublishAsync<T>(T message) where T : class
        {
            return Bus.PublishAsync(message);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> handler) where T : class
        {
            Bus.SubscribeAsync<T>(subscriptionId, handler);
        }
    }
}