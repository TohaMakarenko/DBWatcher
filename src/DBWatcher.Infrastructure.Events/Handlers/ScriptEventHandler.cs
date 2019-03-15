using System;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Events;
using DBWatcher.Core.Messages;
using DBWatcher.Core.Queue;

namespace DBWatcher.Infrastructure.Events.Handlers
{
    public class ScriptEventHandler : IEventHandler<Script, Guid>
    {
        private readonly IMessageBus _messageBus;
        private readonly IMapper _mapper;

        public ScriptEventHandler(IMessageBus messageBus, IMapper mapper)
        {
            _messageBus = messageBus;
            _mapper = mapper;
        }
        public void HandleInsert(Script entity)
        {
            PublishChangeAsync(entity);
        }

        public void HandleUpdate(Script entity)
        {
            PublishChangeAsync(entity);
        }

        public void HandleDelete(Guid id)
        {
            _messageBus.PublishAsync(new ScriptDeleted() {
                Id = id
            });
        }

        private Task PublishChangeAsync(Script script)
        {
            var message = _mapper.Map<ScriptChanged>(script);
            return _messageBus.PublishAsync(message);
        }
    }
}