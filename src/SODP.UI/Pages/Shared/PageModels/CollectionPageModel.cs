using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Text;

namespace SODP.UI.Pages.Shared.PageModels;

public abstract class CollectionPageModel : AppPageModel
{

	public int SelectedPageSize { get; set; }
	public List<SelectListItem> PageSizeList { get; set; } = new List<SelectListItem> { };
	public string SearchString { get; set; }
	public PageInfo PageInfo { get; set; } = new PageInfo();
	public int PageSize { get; set; }

	protected CollectionPageModel(
		IWebAPIProvider apiProvider,
		ILogger<CollectionPageModel> logger,
		IMapper mapper,
		LanguageTranslatorFactory translatorFactory)
		: base(apiProvider, logger, mapper, translatorFactory)
	{
		foreach (var item in PageSizeSelectList.PageSizeList)
		{
			PageSizeList.Add(new SelectListItem(item.ToString(), item.ToString()));
		}
	}

	protected string GetPageUrl(int pageNumber, int pageSize, string searchString)
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

	protected PageInfo GetPageInfo(Page response, string searchString)
	{
		var pageInfo = new PageInfo
		{
			TotalItems = response.TotalCount,
			CurrentPage = response.PageNumber,
			ItemsPerPage = response.PageSize,
			Url = $"{ReturnUrl}?pageNumber=:&pageSize={response.PageSize}"
		};
		if (!string.IsNullOrWhiteSpace(searchString))
		{
			pageInfo.Url += $"&searchString={searchString}";
		}

		return pageInfo;
	}
}
