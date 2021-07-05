using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
using SODP.Model.Enums;

namespace SODP.Api.v0_01.Controllers
{
    [ApiController]
    [Route("api/v0_01/archive-projects")]
    public class ArchiveProjectController : ProjectController
    {
        public ArchiveProjectController(IProjectService projectsService) : base(projectsService)
        {
            _projectsService.SetArchiveMode();
        }

        [HttpGet("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            return Ok(await _projectsService.RestoreAsync(id));
        }
    }
}