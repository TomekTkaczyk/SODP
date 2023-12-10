using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.UI.Api;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Text;

namespace SODP.UI.Pages.Shared.PageModels;

public abstract class CollectionPageModel : AppPageModel
{

	public int SelectedPageSize { get; set; }
	public List<SelectListItem> PageSizeList { get; set; } = new();
	public string SearchString { get; set; } = string.Empty;
	public PageInfo PageInfo { get; set; } = new();
	public int PageSize { get; set; }

	protected CollectionPageModel(
		IWebAPIProvider apiProvider,
		ILogger<CollectionPageModel> logger,
		LanguageTranslatorFactory translatorFactory,
		IMapper mapper)
		: base(apiProvider, logger, translatorFactory, mapper )
	{
		foreach (var item in PageSizeSelectList.PageSizeList)
		{
			PageSizeList.Add(new SelectListItem(item.ToString(), item.ToString()));
		}
	}

	protected virtual string GetPageUrl(int pageNumber, int pageSize, string searchString)
	{
		var url = new StringBuilder();
		url.Append(_endpoint);
		url.Append($"?pageNumber={pageNumber}");
		pageSize = pageSize < 1 ? PageSizeSelectList.PageSizeList[0] : pageSize;
		url.Append($"&pageSize={pageSize}");
		if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
		{
			url.Append($"&searchString={searchString}");
		}

		return url.ToString();
	}

	protected PageInfo GetPageInfo<T>(ApiResponse<Page<T>> response, string searchString)
	{
		var pageInfo = new PageInfo
		{
			TotalItems = response.Value.TotalCount,
			CurrentPage = response.Value.PageNumber,
			ItemsPerPage = response.Value.PageSize,
			Url = $"{ReturnUrl}?pageNumber=:&pageSize={response.Value.PageSize}"
		};
		if (!string.IsNullOrWhiteSpace(searchString))
		{
			pageInfo.Url += $"&searchString={searchString}";
		}

		return pageInfo;
	}
	protected IReadOnlyCollection<T> GetCollection<T>(ApiResponse<Page<T>> response)
	{
		return response.Value.Collection;
	}
}
