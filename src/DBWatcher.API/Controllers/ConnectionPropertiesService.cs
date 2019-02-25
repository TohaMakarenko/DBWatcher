using System;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.API.DTO.Scripts;
using DBWatcher.Core.Entities;
using DBWatcher.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DBWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionPropertiesService : ControllerBase
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public ConnectionPropertiesService(IUnitOfWork work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ConnectionPropertiesDto> GetProperties(Guid id)
        {
            return _mapper.Map<ConnectionPropertiesDto>(await _work.ConnectionPropertiesRepository.Get(id));
        }
    }
}