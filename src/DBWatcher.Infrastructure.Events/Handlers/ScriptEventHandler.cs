using System.Threading.Tasks;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Events;
using DBWatcher.Core.Messages;
using DBWatcher.Core.Queue;

namespace DBWatcher.Infrastructure.Events.Handlers
{
    public class ScriptEventHandler : IEventHandler<Script, int>
    {
        private readonly IMessageBus _messageBus;

        public ScriptEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void HandleInsert(Script entity)
        {
            PublishChangeAsync(entity);
        }

        public void HandleUpdate(Script entity)
        {
            PublishChangeAsync(entity);
        }

        public void HandleDelete(int id)
        {
            _messageBus.PublishAsync(new ScriptDeleted {
                Id = id
            });
        }

        private Task PublishChangeAsync(Script script)
        {
            var message = new ScriptChanged {
                Id = script.Id,
                Body = script.Body
            };
            return _messageBus.PublishAsync(message);
        }
    }
}