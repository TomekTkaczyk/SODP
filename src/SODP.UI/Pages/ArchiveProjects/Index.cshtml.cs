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

namespace SODP.UI.Pages.ArchiveProjects;

[Authorize(Roles = "User, ProjectManager")]
public sealed class IndexModel : ProjectsPageModel
{
	public IndexModel(
		IWebAPIProvider apiProvider,
		ILogger<IndexModel> logger,
		IMapper mapper,
		LanguageTranslatorFactory translatorFactory)
		: base(apiProvider, logger, mapper, translatorFactory)
	{
		ReturnUrl = "/ArchiveProjects";
	}

	public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
	{
		return await OnGetAsync(ProjectStatus.Archival, pageNumber, pageSize, searchString);
	}

	public async Task<IActionResult> OnGetProjectPartialAsync(int id)
	{
		return await GetProjectPartialAsync(id);
	}
}
