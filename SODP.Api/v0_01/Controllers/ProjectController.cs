using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Application.Services;
using System.Threading.Tasks;

namespace SODP.Api.v0_01.Controllers
{
    public abstract class ProjectController : ApiControllerBase
    {
        protected readonly IProjectService _projectsService;

        public ProjectController(IProjectService projectsService, ILogger<ProjectController> logger) : base(logger)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAsync(int currentPage = 1, int pageSize = 15, string searchString = "")
        {
            var response = await _projectsService.GetAllAsync(currentPage, pageSize, searchString);

            return StatusCode( response.StatusCode, response );
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
    }
}
