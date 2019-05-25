using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.API.DTO.Scripts;
using DBWatcher.Core;
using DBWatcher.Core.Execution;
using DBWatcher.Core.Results;
using DBWatcher.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace DBWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IScriptService _scriptService;
        private readonly IUnitOfWork _work;

        public ExecutionController(IUnitOfWork work, IMapper mapper, IScriptService scriptService)
        {
            _work = work;
            _mapper = mapper;
            _scriptService = scriptService;
        }
        
        [HttpPost("Execute")]
        public async Task<ActionResult<ScriptMultipleResult>> ExecuteScript([FromBody] ExecuteScriptDto executeScript)
        {
            var executor =
                await _scriptService.GetScriptExecutor(executeScript.ConnectionId, executeScript.Database);
            var param = _mapper.Map<IEnumerable<Parameter>>(executeScript.Params);
            var script = await _work.ScriptRepository.Get(executeScript.ScriptId);
            if (script == null)
                return NotFound();
            var result = await executor.ExecuteScriptMultiple(script.Body, param);
            return result;
        }
    }
}