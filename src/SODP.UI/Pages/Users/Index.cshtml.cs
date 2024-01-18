using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.Extensions;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Users;

[Authorize(Roles = "Administrator")]
public class IndexModel : CollectionPageModel
{
	public IndexModel(
		IWebAPIProvider apiProvider,
		ILogger<IndexModel> logger,
		LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory)
	{
		ReturnUrl = "/Users";
		_endpoint = "users";
	}
	public ICollection<UserVM> Users { get; set; }

	public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber = 1, int pageSize = 0)
	{
		var endpoint = GetPageUrl(searchString, pageNumber, pageSize);
		var apiResponse = await GetApiResponseAsync<Page<UserDTO>>(endpoint);

		Users = apiResponse.Value.Collection.Select(x => x.ToUserVM()).ToList();
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}
}
