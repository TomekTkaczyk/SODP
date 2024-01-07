using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.JSON;
using SODP.Shared.Response;
using SODP.UI.Api;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared.PageModels;

public abstract class ProjectsPageModel : CollectionPageModel
{
	protected const string _projectDetailsPartialViewName = "PartialView/_ProjectDetailsPartialView";

	protected ProjectsPageModel(
		IWebAPIProvider apiProvider,
		ILogger<ProjectsPageModel> logger,
		LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory)
	{
		_endpoint = "projects";
	}

	[BindProperty]
	public ICollection<ProjectVM> Projects { get; set; }

	protected async Task<IActionResult> GetAsync(ProjectStatus status, string searchString, int pageNumber, int pageSize)
	{
		var endpoint = GetPageUrl(status, searchString, pageNumber, pageSize);
		var apiResponse = await _apiProvider.GetAsync(endpoint);

		var jsonSettings = new JsonSerializerSettings();
		jsonSettings.Converters.Add(new CustomDateOnlyConverter());
		var httpClientSerializer = JsonSerializer.Create(jsonSettings);
		var result = await apiResponse.Content.ReadAsAsync<ApiResponse<Page<ProjectDTO>>>(new[] { new JsonMediaTypeFormatter { SerializerSettings = jsonSettings } });

		Projects = result.Value.Collection.Select(x => x.ToProjectVM()).ToList();
		PageInfo = GetPageInfo(result, searchString);

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
