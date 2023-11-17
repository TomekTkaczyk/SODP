using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.UI.Api;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects;

[Authorize(Roles = "User, ProjectManager")]
public sealed class IndexModel : ProjectsPageModel
{
	const string _newProjectModalViewName = "ModalView/_NewProjectModalView";

	public IndexModel(
		IWebAPIProvider apiProvider,
		ILogger<IndexModel> logger,
		IMapper mapper,
		LanguageTranslatorFactory translatorFactory)
		: base(apiProvider, logger, mapper, translatorFactory)
	{
		ReturnUrl = "/ActiveProjects";
		// _endpoint = "stages";
	}


	public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
	{
		var a = await OnGetAsync(ProjectStatus.Active, pageNumber, pageSize, searchString);

		return a;
	}

	public async Task<IActionResult> OnGetProjectPartialAsync(int id)
	{
		return await GetProjectPartialAsync(id);
	}

	public async Task<IActionResult> OnGetNewProjectAsync()
	{
		return await GetNewProjectPartialViewAsync(new NewProjectVM());
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
					var response = await _apiProvider.GetContent<ApiResponse<ProjectDTO>>(apiResponse);
					if (!response.IsSuccess)
					{
						SetModelErrors(response);
					}
					break;
				default:
					break;
			}
		}

		project.Stages = await GetStagesItems();

		return GetPartialView(project, _newProjectModalViewName);
	}

	private async Task<IActionResult> GetNewProjectPartialViewAsync(NewProjectVM project)
	{
		project.Stages = await GetStagesItems();

		return GetPartialView(project, _newProjectModalViewName);
	}

	private async Task<IEnumerable<SelectListItem>> GetStagesItems()
	{
		var _apiResponse = await GetApiResponseAsync<Page<StageDTO>>("stages");

		return _apiResponse.Value.Collection
			.Where(x => x.ActiveStatus)
			.Select(x => new SelectListItem
			{
				Value = x.Sign,
				Text = x.ToString()
			}); ;
	}
}
