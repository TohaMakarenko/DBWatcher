using System;
using System.Threading.Tasks;
using DBWatcher.Core.Queue;
using EasyNetQ;

namespace DBWatcher.Infrastructure.Rabbit
{
    public class MessageBroker : IMessageBroker
    {
        public MessageBroker(string connectionString)
        {
            Bus = RabbitHutch.CreateBus(connectionString);
            Bus.Advanced.Conventions.ExchangeNamingConvention =
                t => t.ToString();
            Bus.Advanced.Conventions.QueueNamingConvention = (t, sid) => t.ToString() + "_" + sid;
        }

        public IBus Bus { get; set; }

        public Task PublishAsync<T>(T message) where T : class
        {
            return Bus.PublishAsync(message);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> handler) where T : class
        {
            Bus.SubscribeAsync(subscriptionId, handler);
        }
    }
}