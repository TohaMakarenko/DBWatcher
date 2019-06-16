using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.API.DTO.Scripts;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConnectionProperties>>> GetList()
        {
            var connections = await _work.ConnectionPropertiesRepository.GetShortInfoList();
            var result = _mapper.Map<IEnumerable<ConnectionProperties>>(connections);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConnectionProperties>> GetProperties(int id)
        {
            var props = await _work.ConnectionPropertiesRepository.Get(id);
            if (props == null)
                return NotFound();
            return _mapper.Map<ConnectionProperties>(props);
        }

        [HttpPost()]
        public async Task<ActionResult<ConnectionProperties>> InsertProperties(
            [FromBody] ConnectionProperties connectionProperties)
        {
            var result =
                await _work.ConnectionPropertiesRepository.Insert(connectionProperties);
            return Ok(_mapper.Map<ConnectionProperties>(result));
        }
        
        [HttpPut]
        public async Task<ActionResult<ConnectionProperties>> Update([FromBody] ConnectionProperties connection)
        {
            connection = await _work.ConnectionPropertiesRepository.Update(connection);
            return connection;
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _work.ConnectionPropertiesRepository.Delete(id);
            return Ok();
        }
    }
}