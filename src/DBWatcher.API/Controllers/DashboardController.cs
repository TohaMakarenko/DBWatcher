using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DBWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IUnitOfWork _work;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _work = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dashboard>>> Get()
        {
            var result = await _work.DashboardRepository.Get();
            return Ok(result);
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Dashboard>>> Get(int id)
        {
            var result = await _work.DashboardRepository.Get(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<Dashboard>> InsertJob([FromBody] Dashboard job)
        {
            job = await _work.DashboardRepository.Insert(job);
            return Ok(job);
        }
        
        [HttpPut]
        public async Task<ActionResult<Dashboard>> Update([FromBody] Dashboard dashboard)
        {
            dashboard = await _work.DashboardRepository.Update(dashboard);
            return dashboard;
        }

        [HttpDelete("{id:int}")] 
        public async Task<ActionResult> Delete(int id)
        {
            await _work.DashboardRepository.Delete(id);
            return Ok();
        }
    }
}