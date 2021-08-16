using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
using SODP.Shared.DTO;

namespace SODP.Api.v0_01.Controllers
{
    [ApiController]
    [Route("api/v0_01/active-projects")]
    [EnableCors("MyAllowSpecificOrigins")]
    public class ActiveProjectController : ProjectController
    {
        public ActiveProjectController(IProjectService projectsService) : base(projectsService) { }

        [HttpGet("{id}/archive")]
        public async Task<IActionResult> Archive(int id)
        {
            return Ok(await _projectsService.ArchiveAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewProjectDTO project)
        {
            return Ok(await _projectsService.CreateAsync(project));
        }
                                     
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectDTO project)
        {
            if(id != project.Id)
            {
                return BadRequest();
            }

            var response = await _projectsService.UpdateAsync(project);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _projectsService.DeleteAsync(id));
        }

    }
}