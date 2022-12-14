using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ArchiveProjects.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using SODP.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ArchiveProjects
{
    [Authorize(Roles = "User, ProjectManager")]
    public class IndexModel : ProjectsPageModel
    {
		const string projectPartialViewName = "_ProjectPartialView";

		public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, ITranslator translator) : base(apiProvider, logger, mapper, translator)
        {
            ReturnUrl = "/ArchiveProjects";
            _endpoint = "projects";
        }
		public ProjectVM Project { get; set; }

		public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            return await base.OnGetAsync(ProjectStatus.Archival, currentPage, pageSize, searchString);
        }

		public async Task<PartialViewResult> OnGetProjectPartialAsync(int id)
		{
			var apiResponse = await _apiProvider.GetAsync($"projects/{id}/branches");
			var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);
			Project = _mapper.Map<ProjectVM>(response.Data);

			return GetPartialView(Project, projectPartialViewName);
		}
	}
}
