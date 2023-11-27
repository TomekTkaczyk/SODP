using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.UI.Infrastructure;
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
		IMapper mapper, 
		LanguageTranslatorFactory translatorFactory) 
		: base(apiProvider, logger, mapper, translatorFactory) 
	{
		_endpoint = "projects";
	}

	public ProjectDTO Project { get; set; }

	public async Task<IActionResult> OnGetAsync(int id)
	{
		var apiResponse = await GetApiResponseAsync<ProjectDTO>($"{_endpoint}/{id}");

		return apiResponse.Value is null
			? Redirect("/Errors/404")
			: Page();
	}
}
