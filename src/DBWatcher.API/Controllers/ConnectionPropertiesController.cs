using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.API.DTO.Scripts;
using DBWatcher.Core;
using DBWatcher.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DBWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionPropertiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _work;

        public ConnectionPropertiesController(IUnitOfWork work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConnectionPropertiesDto>> GetProperties(int id)
        {
            var props = await _work.ConnectionPropertiesRepository.Get(id);
            if (props == null)
                return NotFound();
            return _mapper.Map<ConnectionPropertiesDto>(props);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<ConnectionPropertiesDto>> InsertProperties(
            [FromBody] ConnectionPropertiesDto connectionProperties)
        {
            var result =
                await _work.ConnectionPropertiesRepository.Insert(
                    _mapper.Map<ConnectionProperties>(connectionProperties));
            return Ok(_mapper.Map<ConnectionPropertiesDto>(result));
        }
    }
}