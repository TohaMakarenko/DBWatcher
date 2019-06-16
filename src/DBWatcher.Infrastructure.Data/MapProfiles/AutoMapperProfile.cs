using AutoMapper;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Results;
using DBWatcher.Infrastructure.Data.Entities;
using Newtonsoft.Json;

namespace DBWatcher.Infrastructure.Data.MapProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<JobLog, JobLogMongo>().ReverseMap();
            CreateMap<ScriptMultipleResult, ScriptResultMongo>()
                .ForMember(dst => dst.Data,
                    c => c.MapFrom(
                        src => JsonConvert.SerializeObject(src.Data)))
                .ReverseMap()
                .ForMember(dst => dst.Data,
                    c => c.MapFrom(
                        src => JsonConvert.DeserializeObject(src.Data)));
        }
    }
}