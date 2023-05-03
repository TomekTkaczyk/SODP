using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.UI.Api;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.PageModels;
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
		IMapper mapper,
		LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
	{
		ReturnUrl = "/Users";
		_endpoint = "users";
	}
	public IReadOnlyCollection<UserDTO> Users { get; set; }

	public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
	{
		var endpoint = GetPageUrl(pageNumber, pageSize, searchString);
		var apiResponse = await GetApiResponseAsync<Page<UserDTO>>(endpoint);

		Users = GetCollection(apiResponse);
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}
}
