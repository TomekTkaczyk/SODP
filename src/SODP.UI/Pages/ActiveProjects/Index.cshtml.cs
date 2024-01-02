using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects;

[Authorize(Roles = "User, ProjectManager")]
public sealed class IndexModel : ProjectsPageModel<ProjectVM>
{
	const string _newProjectModalViewName = "ModalView/_NewProjectModalView";

    public IndexModel(
		IWebAPIProvider apiProvider,
		ILogger<IndexModel> logger,
		LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory)
	{
		ReturnUrl = "/ActiveProjects";
	}

    public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber = 1, int pageSize = 0)
    {
        return await GetAsync(ProjectStatus.Active, searchString, pageNumber,pageSize);
    }

    public async Task<IActionResult> OnGetProjectPartialAsync(int id)
    {
		return await GetProjectPartialAsync<ProjectDetailsVM>(id);
    }

    public async Task<IActionResult> OnGetNewProjectAsync()
	{
		return await GetNewProjectPartialViewAsync();
	}

	public async Task<IActionResult> OnPostNewProjectAsync(NewProjectVM project)
	{
		if (ModelState.IsValid)
		{
			var apiResponse = await _apiProvider.PostAsync(_endpoint, project.ToHttpContent());
			switch (apiResponse.StatusCode)
			{
				case System.Net.HttpStatusCode.Created:
					if (apiResponse.Headers.TryGetValues("Location", out var locationValues))
					{
						var id = locationValues
							.First()
							.Split("/")
							.Last();
						var url = $"/ActiveProjects/Edit?Id={id}#details";
						return new CreatedResult(url, null);
					}
					break;
				case System.Net.HttpStatusCode.Conflict:
					var response = await _apiProvider.GetContentAsync<ApiResponse<ProjectVM>>(apiResponse);
					if (!response.IsSuccess)
					{
						SetModelErrors(response);
					}
					break;
				default:
					break;
			}
		}
		project.Stages = await GetStagesItemsAsync();

		return GetPartialView(project, _newProjectModalViewName);
	}

	private async Task<IActionResult> GetNewProjectPartialViewAsync()
	{
		var project = new NewProjectVM
		{
			Stages = await GetStagesItemsAsync()
		};

		return GetPartialView(project, _newProjectModalViewName);
	}

	private async Task<IEnumerable<SelectListItem>> GetStagesItemsAsync()
	{
		var apiResponse = await GetApiResponseAsync<Page<StageVM>>("stages?ActiveStatus=true");

		return apiResponse.Value.Collection
			.Select(x => new SelectListItem
			{
				Value = x.Sign,
				Text = x.Title
			});
	}
}
