using System;
using System.Threading.Tasks;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Scheduling;
using Microsoft.AspNetCore.Mvc;

namespace DBWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IScriptScheduler _scriptScheduler;
        private readonly IUnitOfWork _work;

        public JobsController(IUnitOfWork work, IScriptScheduler scriptScheduler)
        {
            _work = work;
            _scriptScheduler = scriptScheduler;
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Job>> InsertJob([FromBody] Job job)
        {
            job = await _work.JobRepository.Insert(job);
            return Ok(job);
        }

        [HttpPost("start/{id:int}")]
        public async Task<ActionResult> StartJob(int id)
        {
            await _scriptScheduler.StartJob(id);
            return Ok();
        }

        [HttpPost("Stop/{id:int}")]
        public async Task<ActionResult> StopJob(int id)
        {
            await _scriptScheduler.StopJob(id);
            return Ok();
        }
    }
}