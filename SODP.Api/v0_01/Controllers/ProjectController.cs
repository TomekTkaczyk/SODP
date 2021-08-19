using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SODP.Domain.Services;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    public abstract class ProjectController : ApiControllerBase
    {
        public readonly IProjectService _projectsService;

        public ProjectController(IProjectService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(int currentPage = 1, int pageSize = 15)
        {
            return Ok(await _projectsService.GetAllAsync(currentPage, pageSize));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _projectsService.GetAsync(id);

            return StatusCode(response.StatusCode, response); ;
        }

        [HttpGet("{id}/branches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWithBranchesAsync(int id)
        {
            return Ok(await _projectsService.GetWithBranchesAsync(id));
        }
    }
}
