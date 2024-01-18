using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.Extensions;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Branches;

[Authorize(Roles = "ProjectManager")]
public class IndexModel : CollectionPageModel
{
	const string _editBranchModalViewName = "ModalView/_EditBranchModalView";
	const string _designersPartialViewName = "PartialView/_DesignersPartialView";

	public IndexModel(
		IWebAPIProvider apiProvider,
		ILogger<IndexModel> logger,
		LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory)
	{
		ReturnUrl = "/Branches";
		_endpoint = "branches";
	}

	public ICollection<BranchVM> Branches { get; set; }

	public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber = 1, int pageSize = 0)
	{
		var endpoint = GetPageUrl(searchString, pageNumber, pageSize);
		var apiResponse = await GetApiResponseAsync<Page<BranchDTO>>(endpoint);

		Branches = apiResponse.Value.Collection
			.Select(x => x.ToBranchVM())
			.ToList();
        PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}

	public async Task<IActionResult> OnGetEditBranchAsync(int? id)
	{
		if (!id.HasValue)
		{
			return GetPartialView(new BranchVM(), _editBranchModalViewName);
		}

		var apiResponse = await GetApiResponseAsync<BranchDTO>($"{_endpoint}/{id}");

		if (!apiResponse.IsSuccess)
		{
			return RedirectToPage($"/Errors/{apiResponse.HttpCode}");
		}

		return GetPartialView(apiResponse.Value.ToBranchVM(), _editBranchModalViewName);
	}

	public async Task<IActionResult> OnPostEditBranchAsync(BranchVM model)
	{
		if (ModelState.IsValid)
		{
			var responseMessage = model.Id == 0
				? await _apiProvider.PostAsync($"{_endpoint}", model.ToHttpContent())
				: await _apiProvider.PutAsync($"{_endpoint}/{model.Id}", model.ToHttpContent());
			if (!responseMessage.IsSuccessStatusCode)
			{	   
				// SetError
			}
		}

		return GetPartialView(model, _editBranchModalViewName);
	}

	public async Task<IActionResult> OnGetPartialDesignersAsync(int id)
	{
        var apiResponse = await GetApiResponseAsync<BranchDTO>($"{_endpoint}/{id}/licenses");
		var licenses = apiResponse.Value.Licenses.Select(x => x.ToLicenseVM()).ToList();

        return GetPartialView(licenses, _designersPartialViewName);
	}
}
