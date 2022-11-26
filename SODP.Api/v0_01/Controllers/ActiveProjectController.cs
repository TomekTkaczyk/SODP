using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Model.Enums;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewProjectDTO project)
        {
            var result = await _projectsService.CreateAsync(project);
            return result.StatusCode switch
            {
                StatusCodes.Status200OK => Ok(result),
                StatusCodes.Status403Forbidden => Forbid(),
                StatusCodes.Status500InternalServerError => StatusCode(StatusCodes.Status500InternalServerError),
                _ => BadRequest(result),
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


        [HttpGet("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetBranchRolesesAsync(int id, int branchId)
        {
            var serviceResponse = await _projectsService.GetBranchRolesAsync(id, branchId);

            return Ok(serviceResponse);
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


        [HttpPatch("{id}/branches/{branchId}/designer/{licenseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> SetBranchDesignerAsync(int id, int branchId, int licenseId)
        {
            var serviceResponse = await _projectsService.SetBranchTechnicalRoleAsync(id, branchId, TechnicalRole.Designer, licenseId);

            return Ok(serviceResponse);
        }


        [HttpPatch("{id}/branches/{branchId}/checker/{licenseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> SetBranchCheckerAsync(int id, int branchId, int licenseId)
        {
            var serviceResponse = await _projectsService.SetBranchTechnicalRoleAsync(id, branchId, TechnicalRole.Checker, licenseId);

            return Ok(serviceResponse);
        }


        [HttpPatch("{id}/branches/{branchId}/role/{role}/{licenseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> SetBranchRoleAsync(int id, int branchId, TechnicalRole role, int licenseId)
        {
            var serviceResponse = await _projectsService.SetBranchTechnicalRoleAsync(id, branchId, role, licenseId);

            return Ok(serviceResponse);
        }
    }
}