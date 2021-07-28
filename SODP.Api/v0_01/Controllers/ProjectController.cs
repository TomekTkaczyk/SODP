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
            //var req = HttpContext.Request.Query;
            //int.TryParse(req["page_number"], out int page_number);
            //int.TryParse(req["page_size"], out int page_size);

            return Ok(await _projectsService.GetAllAsync(currentPage, pageSize));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsync(int Id)
        {
            return Ok(await _projectsService.GetAsync(Id));
        }

        [HttpGet("{Id}/branches")]
        public async Task<IActionResult> GetWithBranchesAsync(int Id)
        {
            return Ok(await _projectsService.GetWithBranchesAsync(Id));
        }
    }
}
