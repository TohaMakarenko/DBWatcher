using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DBWatcher.API.DTO;
using DBWatcher.API.DTO.Scripts;
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
            var scripts = _mapper.Map<IEnumerable<ScriptInfoDto>>(await _work.ScriptRepository.Get());
            var folders = await _work.FolderRepository.Get();
            var result = new List<FolderDto>(folders.Count());
            result.Add(new FolderDto {
                Id = -1,
                Name = "All",
                Scripts = scripts
            });
            foreach (var folder in folders) {
                var dto = _mapper.Map<FolderDto>(folder);
                dto.Scripts = scripts.Where(x => folder.Scripts.Contains(x.Id))
                    .ToArray();
                result.Add(dto);
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Folder>> Insert([FromBody] Folder folder)
        {
            folder = await _work.FolderRepository.Insert(folder);
            return folder;
        }
    }
}