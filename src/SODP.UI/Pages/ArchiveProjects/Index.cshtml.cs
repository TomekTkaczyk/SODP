using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ArchiveProjects
{
    [Authorize(Roles = "User, ProjectManager")]
    public class IndexModel : ProjectsPageModel
    {
		const string _projectPartialViewName = "PartialView/_ProjectPartialView";

		public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
        {
            ReturnUrl = "/ArchiveProjects";
        }
		public ProjectVM Project { get; set; }

		public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            return await base.OnGetAsync(ProjectStatus.Archival, currentPage, pageSize, searchString);
        }

		public async Task<PartialViewResult> OnGetProjectPartialAsync(int id)
		{
			var apiResponse = await _apiProvider.GetAsync($"projects/{id}/details");
			var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);
			Project = _mapper.Map<ProjectVM>(response.Data);

			return GetPartialView(Project, _projectPartialViewName);
		}
	}
}
