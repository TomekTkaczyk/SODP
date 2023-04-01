using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared.PageModels;

public abstract class ProjectsPageModel : CollectionPageModel
{

	public ICollection<ProjectDTO> Projects { get; set; }

	protected ProjectsPageModel(
		IWebAPIProvider apiProvider,
		ILogger<ProjectsPageModel> logger,
		IMapper mapper,
		LanguageTranslatorFactory translatorFactory)
		: base(apiProvider, logger, mapper, translatorFactory)
	{
		_endpoint = "projects";
	}

	protected async Task<IActionResult> OnGetAsync(ProjectStatus status, int pageNumber = 1, int pageSize = 0, string searchString = "")
	{
		var endpoint = GetPageUrl(status, pageNumber, pageSize, searchString);
		var apiResponse = await GetApiResponseAsync<Page<ProjectDTO>>(endpoint);

		Projects = GetCollection(apiResponse);
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}

	private string GetPageUrl(ProjectStatus status, int pageNumber, int pageSize, string searchString)
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
