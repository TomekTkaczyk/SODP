using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ArchiveProjects.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ArchiveProjects;

[Authorize(Roles = "Administrator,ProjectManager,User")]
public class EditModel : AppPageModel
{

	public EditModel(
		IWebAPIProvider apiProvider, 
		ILogger<EditModel> logger, 
		LanguageTranslatorFactory translatorFactory,
		IMapper mapper)
		: base(apiProvider, logger, translatorFactory, mapper)
	{
		_endpoint = "projects";
	}

	public ProjectDetailsVM Project { get; set; }

	public async Task<IActionResult> OnGetAsync(int id)
	{
		var apiResponse = await GetApiResponseAsync<ProjectDetailsVM>($"{_endpoint}/{id}/details");

		return apiResponse.Value is null
			? Redirect("/Errors/404")
			: Page();
	}
}
