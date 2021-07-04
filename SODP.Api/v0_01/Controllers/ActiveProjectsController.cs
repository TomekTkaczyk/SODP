using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
using SODP.Shared.DTO;

namespace SODP.Api.v0_01.Controllers
{
    [ApiController]
    [Route("api/v0_01/active-projects")]
    public class ActiveProjectsController : ProjectsController
    {
        public ActiveProjectsController(IProjectsService projectsService) : base(projectsService) { }

        [HttpGet("{id}/archive")]
        public async Task<IActionResult> Archive(int id)
        {
            return Ok(await _projectsService.ArchiveAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectDTO project)
        {
            return Ok(await _projectsService.CreateAsync(project));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _projectsService.DeleteAsync(id));
        }

    }
}