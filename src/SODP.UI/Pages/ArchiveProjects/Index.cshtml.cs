using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.Enums;
using SODP.UI.Api;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ArchiveProjects.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ArchiveProjects;

[Authorize(Roles = "User, ProjectManager")]
public sealed class IndexModel : ProjectsPageModel<ProjectVM>
{
    public IndexModel(
		IWebAPIProvider apiProvider,
		ILogger<IndexModel> logger,
		LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory)
	{
		ReturnUrl = "/ArchiveProjects";
	}


    public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber = 1, int pageSize = 0)
    {
		return await GetAsync(ProjectStatus.Archival, searchString, pageNumber, pageSize);
	}

	public async Task<IActionResult> OnGetProjectPartialAsync(int id)
	{
        return await GetProjectPartialAsync<ProjectDetailsVM>(id);
	}
}
