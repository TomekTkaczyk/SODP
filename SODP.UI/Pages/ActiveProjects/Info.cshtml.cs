using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using SODP.UI.Pages.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects
{
    [Authorize(Roles = "Administrator,ProjectManager")]
    public class InfoModel : SODPPageModel
    {
        private readonly IProjectService _projectsService;

        public InfoModel(IProjectService projectsService, ILogger<InfoModel> logger) : base(logger) 
        {
            _projectsService = projectsService;
        }

        public ProjectDTO Project { get; set; }
        public IList<string> Branches { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _projectsService.GetAsync(id);
            Project = response.Data;

            return Page();
        }
    }
}
