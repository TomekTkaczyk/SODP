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
    // [Authorize]
    [ApiController]
    [Route("api/v0_01/projects")]
    public class ProjectController : ApiControllerBase
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service, ILogger<ProjectController> logger) : base(logger) 
        {
            _service = service;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPageAsync(ProjectStatus status = ProjectStatus.Active, int currentPage = 1, int pageSize = 0,  string searchString = "")
        {
            return Ok( await ((IProjectService)_service).GetPageAsync(status, currentPage, pageSize, searchString));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _service.GetAsync(id));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] NewProjectDTO project)
        {
            var result = await (_service as IProjectService).CreateAsync(project);
            return result.StatusCode switch
            {
                StatusCodes.Status200OK => Ok(result),
                StatusCodes.Status403Forbidden => Forbid(),
                StatusCodes.Status500InternalServerError => StatusCode(StatusCodes.Status500InternalServerError),
                _ => BadRequest(result),
            };
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }


        [HttpGet("{id}/branches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetWithBranchesAsync(int id)
        {
            return Ok(await ((IProjectService)_service).GetWithBranchesAsync(id));
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

            var response = await _service.UpdateAsync(project);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }

            return NoContent();
        }


        //[HttpGet("{id}/branches/{branchId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //public async Task<IActionResult> GetBranchRolesesAsync(int id, int branchId)
        //{                                               
        //    return Ok(await _service.GetBranchRolesAsync(id, branchId));
        //}


        //[HttpPut("{id}/branches/{branchId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //public async Task<IActionResult> AddBranchAsync(int id, int branchId)
        //{
        //    return Ok(await _service.AddBranchAsync(id, branchId));
        //}


        //[HttpDelete("{id}/branches/{branchId}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //public async Task<IActionResult> DeleteBranchAsync(int id, int branchId)
        //{
        //    return Ok(await _service.DeleteBranchAsync(id, branchId));
        //}


        //[HttpPatch("{id}/branch/{branchId}/role/{role}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //public async Task<IActionResult> SetBranchRoleAsync(int id, int branchId, TechnicalRole role, [FromBody] TechnicalRoleDTO technicalRole)
        //{
        //    if (id != technicalRole.ProjectId || branchId != technicalRole.BranchId || role != technicalRole.Role)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(await _service.SetBranchTechnicalRoleAsync(technicalRole));
        //}


        [HttpPatch("{id}/archive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Archive(int id)
        {
            return Ok(await ((IProjectService)_service).ArchiveAsync(id));
        }


        [HttpPatch("{id}/restore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Restore(int id)
        {
            return Ok(await ((IProjectService)_service).RestoreAsync(id));
        }


		[HttpPatch("{id}/investor")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> SetInvestorAsync(int id, [FromBody] string investor)
		{
			return Ok(await ((IProjectService)_service).SetInvestorAsync(id, investor));
		}
	}
}
