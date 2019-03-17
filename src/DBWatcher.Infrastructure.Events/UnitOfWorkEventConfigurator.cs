using System;
using DBWatcher.Core;
using DBWatcher.Infrastructure.Events.Handlers;

namespace DBWatcher.Infrastructure.Events
{
    public static class UnitOfWorkEventConfigurator
    {
        public static IUnitOfWork AddEventHandlers(IUnitOfWork work, IServiceProvider provider)
        {
            var scriptEventHandler = new ScriptEventHandler(work.Bus);
            work.ScriptRepository.OnInsert += scriptEventHandler.HandleInsert;
            work.ScriptRepository.OnUpdate += scriptEventHandler.HandleUpdate;
            work.ScriptRepository.OnDelete += scriptEventHandler.HandleDelete;
            var propertiesEventHandler = new ConnectionPropertiesEventHandler(work.Bus);
            work.ConnectionPropertiesRepository.OnInsert += propertiesEventHandler.HandleInsert;
            work.ConnectionPropertiesRepository.OnUpdate += propertiesEventHandler.HandleUpdate;
            work.ConnectionPropertiesRepository.OnDelete += propertiesEventHandler.HandleDelete;

            return work;
        }
    }
}