using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.Enums;
using SODP.UI.Api;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared.PageModels;

public abstract class ProjectsPageModel<T> : CollectionPageModel
{
	protected const string _projectPartialViewName = "PartialView/_ProjectPartialView";

	protected ProjectsPageModel(
		IWebAPIProvider apiProvider,
		ILogger<ProjectsPageModel<T>> logger,
		LanguageTranslatorFactory translatorFactory,
		IMapper mapper)
		: base(apiProvider, logger, translatorFactory, mapper)
	{
		_endpoint = "projects";
	}

	protected T Project { get; set; }

	public IReadOnlyCollection<T> Projects { get; set; }

	protected async Task<IActionResult> GetAsync(ProjectStatus status, int pageNumber = 1, int pageSize = 0, string searchString = "")
	{
		var endpoint = GetPageUrl(status, pageNumber, pageSize, searchString);
		var apiResponse = await GetApiResponseAsync<Page<T>>(endpoint);

		Projects = GetCollection(apiResponse);
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}

	protected async Task<IActionResult> GetProjectPartialAsync<TDetail>(int id)
	{
		var apiResponse = await _apiProvider.GetAsync($"{_endpoint}/{id}/details");
		var response = await _apiProvider.GetContentAsync<ApiResponse<TDetail>>(apiResponse);

		return GetPartialView(response.Value, _projectPartialViewName);
	}

	protected string GetPageUrl(ProjectStatus status, int pageNumber, int pageSize, string searchString)
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
