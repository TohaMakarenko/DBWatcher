using System;
using AutoMapper;
using DBWatcher.Core;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Events;
using DBWatcher.Core.Queue;
using DBWatcher.Infrastructure.Events.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DBWatcher.Infrastructure.Events
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IEventHandler<Script, Guid>>(provider => {
                var bus = provider.GetService<IMessageBus>();
                var mapper = provider.GetService<IMapper>();
                var unitOfWork = provider.GetService<IUnitOfWork>();
                var handler = new ScriptEventHandler(bus, mapper);
                unitOfWork.ScriptRepository.OnInsert += handler.HandleInsert;
                unitOfWork.ScriptRepository.OnUpdate += handler.HandleUpdate;
                unitOfWork.ScriptRepository.OnDelete += handler.HandleDelete;
                return handler;
            });
            
            services.AddSingleton<IEventHandler<ConnectionProperties, Guid>>(provider => {
                var bus = provider.GetService<IMessageBus>();
                var mapper = provider.GetService<IMapper>();
                var unitOfWork = provider.GetService<IUnitOfWork>();
                var handler = new ConnectionPropertiesEventHandler(bus, mapper);
                unitOfWork.ConnectionPropertiesRepository.OnInsert += handler.HandleInsert;
                unitOfWork.ConnectionPropertiesRepository.OnUpdate += handler.HandleUpdate;
                unitOfWork.ConnectionPropertiesRepository.OnDelete += handler.HandleDelete;
                return handler;
            });

            return services;
        }
    }
}