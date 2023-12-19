using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.UI.Api;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Users.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Users;

[Authorize(Roles = "Administrator")]
public class IndexModel : CollectionPageModel
{
	public IndexModel(
		IWebAPIProvider apiProvider,
		ILogger<IndexModel> logger,
		LanguageTranslatorFactory translatorFactory,
		IMapper mapper) : base(apiProvider, logger, translatorFactory, mapper)
	{
		ReturnUrl = "/Users";
		_endpoint = "users";
	}
	public ICollection<UserVM> Users { get; set; }

	public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber = 1, int pageSize = 0)
	{
		var endpoint = GetPageUrl(searchString, pageNumber, pageSize);
		var apiResponse = await GetApiResponseAsync<Page<UserVM>>(endpoint);

		Users = GetCollection(apiResponse);
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}
}
