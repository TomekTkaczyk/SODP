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
        public async Task<IActionResult> GetAllAsync(int currentPage = 1, int pageSize = 15)
        {
            return Ok(await _projectsService.GetAllAsync(currentPage, pageSize));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsync(int Id)
        {
            var response = await _projectsService.GetAsync(Id);

            return StatusCode(response.StatusCode, response); ;
        }

        [HttpGet("{Id}/branches")]
        public async Task<IActionResult> GetWithBranchesAsync(int Id)
        {
            return Ok(await _projectsService.GetWithBranchesAsync(Id));
        }
    }
}
