using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.API.DTO.Scripts;
using DBWatcher.Core;
using DBWatcher.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DBWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScriptsController : ControllerBase
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public ScriptsController(IUnitOfWork work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ScriptDto> GetScript(Guid id)
        {
            return _mapper.Map<ScriptDto>(await _work.ScriptRepository.Get(id));
        }
    }
}