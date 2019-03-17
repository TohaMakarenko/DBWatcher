using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Events;
using DBWatcher.Core.Messages;
using DBWatcher.Core.Queue;

namespace DBWatcher.Infrastructure.Events.Handlers
{
    public class ConnectionPropertiesEventHandler : IEventHandler<ConnectionProperties, int>
    {
        private readonly IMessageBus _messageBus;

        public ConnectionPropertiesEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void HandleInsert(ConnectionProperties entity)
        {
            PublishChangeAsync(entity);
        }

        public void HandleUpdate(ConnectionProperties entity)
        {
            PublishChangeAsync(entity);
        }

        public void HandleDelete(int id)
        {
            _messageBus.PublishAsync(new ConnectionPropertiesDeleted {
                Id = id
            });
        }

        private Task PublishChangeAsync(ConnectionProperties props)
        {
            var message = new ConnectionPropertiesChanged {
                Id = props.Id,
                Login = props.Login,
                Server = props.Server,
                Password = props.Password,
                IsPasswordEncrypted = props.IsPasswordEncrypted
            };
            return _messageBus.PublishAsync(message);
        }
    }
}