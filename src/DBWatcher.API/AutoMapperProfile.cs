using AutoMapper;
using DBWatcher.API.DTO.Scripts;
using DBWatcher.Core.Entities;
using DBWatcher.Core.ScriptExecutor;

namespace DBWatcher.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ConnectionPropertiesDto, ConnectionProperties>();
            CreateMap<ConnectionProperties, ConnectionPropertiesDto>();
            CreateMap<ParameterDto, Parameter>();
        }
    }
}