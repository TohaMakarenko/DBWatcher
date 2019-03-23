using AutoMapper;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Messages;

namespace DBWatcher.Infrastructure.Events
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Script, ScriptChanged>();
            CreateMap<ConnectionProperties, ConnectionPropertiesChanged>();
        }
    }
}