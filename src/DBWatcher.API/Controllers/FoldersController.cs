using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.API.DTO;
using DBWatcher.Core;
using DBWatcher.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DBWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _work;

        public FoldersController(IUnitOfWork work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FolderDto>>> Get()
        {
            var folders = await _work.FolderRepository.Get();
            var result = _mapper.Map<IEnumerable<FolderDto>>(folders);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Folder>> Insert([FromBody] Folder folder)
        {
            folder = await _work.FolderRepository.Insert(folder);
            return folder;
        }

        [HttpPost("{id}/removeScript")]
        public async Task<ActionResult<Folder>> RemoveScript(int id, [FromBody] int scriptId)
        {
            return await _work.FolderRepository.RemoveScript(id, scriptId);
        }

        [HttpPost("{id}/addScript")]
        public async Task<ActionResult<Folder>> AddScript(int id, [FromBody] int scriptId)
        {
            return await _work.FolderRepository.AddScript(id, scriptId);
        }
    }
}