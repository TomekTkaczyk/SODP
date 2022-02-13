using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Domain.Services;
using SODP.Model.Enums;

namespace SODP.Api.v0_01.Controllers
{
    [ApiController]
    [Route("api/v0_01/archive-projects")]
    [EnableCors("SODPOriginsSpecification1")]
    public class ArchiveProjectController : ProjectController
    {
        public ArchiveProjectController(IProjectService projectsService, ILogger<ActiveProjectController> logger) : base(projectsService, logger)
        {
            _projectsService.SetArchiveMode();
        }

        [HttpGet("{id}/restore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Restore(int id)
        {
            return Ok(await _projectsService.RestoreAsync(id));
        }
    }
}