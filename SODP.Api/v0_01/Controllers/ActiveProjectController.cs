using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    [ApiController]
    [Route("api/v0_01/active-projects")]
    [EnableCors("SODPOriginsSpecification")]
    public class ActiveProjectController : ProjectController
    {
        public ActiveProjectController(IProjectService projectsService, ILogger<ActiveProjectController> logger) : base(projectsService, logger) { }

        [HttpGet("{id}/archive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Archive(int id)
        {
            return Ok(await _projectsService.ArchiveAsync(id));
        }

        // To do poprawy

        //[HttpPatch("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //public async Task<IActionResult> Archive(int id, [FromBody] { Status: Archive})
        //{
        //    return Ok(await _projectsService.ArchiveAsync(id));
        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] NewProjectDTO project)
        {
            var result = await _projectsService.CreateAsync(project);
            return result.StatusCode switch
            {
                StatusCodes.Status200OK => Ok(result),
                StatusCodes.Status403Forbidden => Forbid(),
                _ => BadRequest(project),
            };
        }
                                     
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _projectsService.DeleteAsync(id));
        }

        [HttpPut("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddBranchAsync(int id, int branchId)
        {
            var serviceResponse = await _projectsService.AddBranchAsync(id, branchId);

            return Ok(serviceResponse);
        }

        [HttpDelete("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteBranchAsync(int id, int branchId)
        {
            var serviceResponse = await _projectsService.DeleteBranchAsync(id, branchId);

            return Ok(serviceResponse);
        }
    }
}