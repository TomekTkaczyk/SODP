using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.JSON;
using SODP.Shared.Response;
using SODP.UI.Api;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.Extensions;
using SODP.UI.Pages.Shared.ViewModels;
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
	public IEnumerable<ProjectVM> Projects { get; set; }

	protected async Task<IActionResult> GetAsync(ProjectStatus status, string searchString, int pageNumber, int pageSize)
	{
		var endpoint = GetPageUrl(status, searchString, pageNumber, pageSize);
		var apiResponse = await GetApiResponseAsync<Page<ProjectDTO>>(endpoint);

		Projects = apiResponse.Value.Collection.Select(x => x.ToProjectVM()).ToList();
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}

	protected async Task<IActionResult> GetProjectPartialAsync(int id)
	{
		var apiResponse = await GetApiResponseAsync<ProjectDTO>($"{_endpoint}/{id}/details");

		var viewModel = apiResponse.Value.ToProjectDetailsVM();

		return GetPartialView(viewModel, _projectDetailsPartialViewName);
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
