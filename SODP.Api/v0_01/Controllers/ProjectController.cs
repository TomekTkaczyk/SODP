using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    [ApiController]
    [Route("api/v0_01/projects")]
    [EnableCors("SODPOriginsSpecification")]
    public class ProjectController : ApiControllerBase
    {
        private readonly IProjectService _projectsService;

        public ProjectController(IProjectService projectsService, ILogger<ProjectController> logger) : base(logger)
        {
            _projectsService = projectsService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync(ProjectStatus status, int currentPage = 1, int pageSize = 15, string searchString = "")
        {
            switch (status)
            {
                case ProjectStatus.Active:
                    _projectsService.SetActiveMode();
                    break;
                case ProjectStatus.Archived:
                    _projectsService.SetArchiveMode();
                    break;
                default:
                    return BadRequest();
            }
            var response = await _projectsService.GetAllAsync(currentPage, pageSize, searchString);

            return StatusCode(response.StatusCode, response);
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


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _projectsService.GetAsync(id);

            return StatusCode(response.StatusCode, response); ;
        }


        [HttpGet("{id}/branches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetWithBranchesAsync(int id)
        {
            return Ok(await _projectsService.GetWithBranchesAsync(id));
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


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectDTO project)
        {
            if (id != project.Id)
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


        [HttpPut("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddBranchAsync(int id, int branchId)
        {
            var serviceResponse = await _projectsService.AddBranchAsync(id, branchId);

            return Ok(serviceResponse);
        }


        [HttpPatch("{id}/branch/{branchId}/role/{role}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> SetBranchRoleAsync(int id, int branchId, TechnicalRole role, [FromBody] TechnicalRoleDTO technicalRole)
        {
            if (id != technicalRole.ProjectId || branchId != technicalRole.BranchId || role != technicalRole.Role)
            {
                return BadRequest();
            }

            var serviceResponse = await _projectsService.SetBranchTechnicalRoleAsync(technicalRole);

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


        [HttpDelete("{id}/branches/{branchId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteBranchAsync(int id, int branchId)
        {
            var serviceResponse = await _projectsService.DeleteBranchAsync(id, branchId);

            return Ok(serviceResponse);
        }


        [HttpPatch("{id}/archive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Archive(int id)
        {
            return Ok(await _projectsService.ArchiveAsync(id));
        }


        [HttpPatch("{id}/restore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Restore(int id)
        {
            return Ok(await _projectsService.RestoreAsync(id));
        }
    }
}
