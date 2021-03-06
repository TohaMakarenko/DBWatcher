using AutoMapper;
using DBWatcher.API.DTO;
using DBWatcher.API.DTO.Scripts;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Execution;

namespace DBWatcher.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ConnectionPropertiesDto, ConnectionProperties>();
            CreateMap<ConnectionProperties, ConnectionPropertiesDto>();
            CreateMap<ParameterDto, Parameter>();
            CreateMap<Script, ScriptInfoDto>();
            CreateMap<Script, ScriptDto>();
            CreateMap<Folder, FolderDto>();
            CreateMap<Parameter, ParameterDto>().ReverseMap();
        }
    }
}