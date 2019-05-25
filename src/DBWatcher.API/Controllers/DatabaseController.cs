using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.Core;
using DBWatcher.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DBWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseService _databaseService;
        private readonly IUnitOfWork _work;

        public DatabaseController(IUnitOfWork work, IMapper mapper, IDatabaseService databaseService)
        {
            _work = work;
            _mapper = mapper;
            _databaseService = databaseService;
        }

        [HttpGet("{connectionId}/Databases")]
        public async Task<ActionResult<IEnumerable<string>>> GetDatabases(int connectionId)
        {
            var connection = await _work.ConnectionPropertiesRepository.Get(connectionId);
            if (connection == null)
                return NotFound();
            var databases = await _databaseService.GetDatabases(connection);
            return Ok(databases);
        }
    }
}