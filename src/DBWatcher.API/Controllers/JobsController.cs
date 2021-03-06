using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
using DBWatcher.Core.Dto.Jobs;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> Get()
        {
            var jobs = await _work.JobRepository.Get();
            return Ok(jobs);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Job>> Get(int id)
        {
            var job = await _work.JobRepository.Get(id);
            return Ok(job);
        }

        [HttpPost]
        public async Task<ActionResult<Job>> InsertJob([FromBody] Job job)
        {
            job = await _work.JobRepository.Insert(job);
            return Ok(job);
        }
        
        [HttpPut]
        public async Task<ActionResult<Job>> Update([FromBody] Job job)
        {
            job = await _work.JobRepository.Update(job);
            return job;
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _work.JobRepository.Delete(id);
            return Ok();
        }

        [HttpPost("{id:int}/start")]
        public async Task<ActionResult> StartJob(int id)
        {
            await _scriptScheduler.StartJob(id);
            return Ok();
        }

        [HttpPost("{id:int}/stop")]
        public async Task<ActionResult> StopJob(int id)
        {
            await _scriptScheduler.StopJob(id);
            return Ok();
        }
        
        [HttpGet("{id:int}/log/{skip:int?}/{take:int?}")]
        public async Task<ActionResult<IEnumerable<JobLog>>> GetLog(int id, int skip = 0, int take = 100)
        {
            var result = await _work.JobLogRepository.GetForJob(id, skip, take);
            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<JobLog>>> GetLog([FromBody] JobLogSearchFilter filter)
        {
            var result = await _work.JobLogRepository.Search(filter);
            return Ok(result);
        }
    }
}