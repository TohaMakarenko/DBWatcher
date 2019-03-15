using System;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Events;
using DBWatcher.Core.Messages;
using DBWatcher.Core.Queue;

namespace DBWatcher.Infrastructure.Events.Handlers
{
    public class ConnectionPropertiesEventHandler : IEventHandler<ConnectionProperties, Guid>
    {
        private readonly IMessageBus _messageBus;
        private readonly IMapper _mapper;

        public ConnectionPropertiesEventHandler(IMessageBus messageBus, IMapper mapper)
        {
            _messageBus = messageBus;
            _mapper = mapper;
        }

        public void HandleInsert(ConnectionProperties entity)
        {
            PublishChangeAsync(entity);
        }

        public void HandleUpdate(ConnectionProperties entity)
        {
            PublishChangeAsync(entity);
        }

        public void HandleDelete(Guid id)
        {
            _messageBus.PublishAsync(new ConnectionPropertiesDeleted() {
                Id = id
            });
        }

        private Task PublishChangeAsync(ConnectionProperties props)
        {
            var message = _mapper.Map<ConnectionPropertiesChanged>(props);
            return _messageBus.PublishAsync(message);
        }
    }
}