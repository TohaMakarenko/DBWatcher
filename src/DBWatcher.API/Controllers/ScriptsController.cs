using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.API.DTO.Scripts;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Execution;
using DBWatcher.Core.Results;
using DBWatcher.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DBWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScriptsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IScriptService _scriptService;
        private readonly IUnitOfWork _work;

        public ScriptsController(IUnitOfWork work, IMapper mapper, IScriptService scriptService)
        {
            _work = work;
            _mapper = mapper;
            _scriptService = scriptService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScriptDto>> GetScript(int id)
        {
            var script = await _work.ScriptRepository.Get(id);
            if (script == null)
                return NotFound();
            return _mapper.Map<ScriptDto>(script);
        }

        [HttpPost("Execute")]
        public async Task<ActionResult<ScriptMultipleResult>> ExecuteScript([FromBody] ExecuteScriptDto executeScript)
        {
            var executor =
                await _scriptService.GetScriptExecutor(executeScript.ConnectionPropsId, executeScript.Database);
            var param = _mapper.Map<IEnumerable<Parameter>>(executeScript.Params);
            return await executor.ExecuteScriptMultiple(executeScript.Body, param);
        }

        [HttpPost]
        public async Task<ActionResult<Script>> InsertScript([FromBody] Script script)
        {
            script = await _work.ScriptRepository.Insert(script);
            return script;
        }
    }
}