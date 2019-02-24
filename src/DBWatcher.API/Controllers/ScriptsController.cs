using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBWatcher.API.DTO.Scripts;
using DBWatcher.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DBWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScriptsController : ControllerBase
    {
        private readonly IUnitOfWork _work;

        public ScriptsController(IUnitOfWork work)
        {
            _work = work;
        }
        
    }
}