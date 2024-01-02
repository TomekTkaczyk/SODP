using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Api;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared.PageModels;

public abstract class ProjectsPageModel<T> : CollectionPageModel
{
	protected const string _projectDetailsPartialViewName = "PartialView/_ProjectDetailsPartialView";

	protected ProjectsPageModel(
		IWebAPIProvider apiProvider,
		ILogger<ProjectsPageModel<T>> logger,
		LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory)
	{
		_endpoint = "projects";
	}

	protected T Project { get; set; }

	public ICollection<T> Projects { get; set; }

	protected async Task<IActionResult> GetAsync(ProjectStatus status, string searchString, int pageNumber, int pageSize)
	{
		var endpoint = GetPageUrl(status, searchString, pageNumber, pageSize);
		var apiResponse = await GetApiResponseAsync<Page<T>>(endpoint);

		Projects = GetCollection(apiResponse);
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}

	protected async Task<IActionResult> GetProjectPartialAsync<TDetail>(int id)
	{
		var apiResponse = await GetApiResponseAsync<TDetail>($"{_endpoint}/{id}/details");

		return GetPartialView(apiResponse.Value, _projectDetailsPartialViewName);
	}

	protected string GetPageUrl(ProjectStatus status, string searchString, int pageNumber, int pageSize)
	{
		var url = new StringBuilder();
		url.Append(_endpoint);
		url.Append($"?pageNumber={pageNumber}");
		pageSize = pageSize < 1 ? PageSizeSelectList.PageSizeList[0] : pageSize;
		url.Append($"&pageSize={pageSize}");
		url.Append($"&status={status}");
		if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
		{
			url.Append($"&searchString={searchString}");
		}

		return url.ToString();
	}
}
