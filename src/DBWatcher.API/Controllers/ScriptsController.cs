﻿using System.Collections.Generic;
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

        public async Task<ActionResult<IEnumerable<ScriptInfoDto>>> GetList()
        {
            var scripts = await _work.ScriptRepository.GetShortInfo();
            var result = _mapper.Map<IEnumerable<ScriptInfoDto>>(scripts);
            return Ok(result);
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
        public async Task<ActionResult<Script>> Insert([FromBody] Script script)
        {
            script = await _work.ScriptRepository.Insert(script);
            return script;
        }

        [HttpPut]
        public async Task<ActionResult<Script>> Update([FromBody] Script script)
        {
            script = await _work.ScriptRepository.Update(script);
            return script;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _work.ScriptRepository.Delete(id);
            return Ok();
        }
    }
}