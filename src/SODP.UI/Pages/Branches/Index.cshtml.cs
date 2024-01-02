using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Branches.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Net.Http;
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
		var apiResponse = await GetApiResponseAsync<Page<BranchVM>>(endpoint);

		Branches = GetCollection(apiResponse);
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}

	public async Task<IActionResult> OnGetEditBranchAsync(int? id)
	{
		if (!id.HasValue)
		{
			return GetPartialView(new BranchVM(), _editBranchModalViewName);
		}

		var apiResponse = await GetApiResponseAsync<BranchVM>($"{_endpoint}/{id}");

		if (!apiResponse.IsSuccess)
		{
			return RedirectToPage($"/Errors/{apiResponse.HttpCode}");
		}

		return GetPartialView(apiResponse.Value, _editBranchModalViewName);
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
		var responseMessage = await _apiProvider.GetAsync($"{_endpoint}/{id}/licenses");
		if (!responseMessage.IsSuccessStatusCode)
		{
			return RedirectToPage($"/Errors/{responseMessage.StatusCode}");
		}

		var apiResponse = await responseMessage.Content.ReadAsAsync<ApiResponse<Page<LicenseVM>>>();																		

		return GetPartialView(apiResponse.Value.Collection, _designersPartialViewName);
	}
}
