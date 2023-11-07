using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.UI.Api;
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
		IMapper mapper,
		LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
	{
		ReturnUrl = "/Branches";
		_endpoint = "branches";
	}

	public IReadOnlyCollection<BranchVM> Branches { get; set; }

	public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
	{
		var endpoint = GetPageUrl(pageNumber, pageSize, searchString);
		var apiResponse = await GetApiResponseAsync<Page<BranchDTO>>(endpoint);

		Branches = _mapper.Map<IReadOnlyCollection<BranchVM>>(apiResponse.Value.Collection);
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}

	public async Task<IActionResult> OnGetEditBranchAsync(int? id)
	{
		var model = new BranchVM();
		if (id.HasValue)
		{
			var responseMessage = await _apiProvider.GetAsync($"{_endpoint}/{id}");
			if(!responseMessage.IsSuccessStatusCode) 
			{
				// SetError
				return RedirectToPage($"/Errors/{responseMessage.StatusCode}");
			}

			var apiResponse = await responseMessage.Content.ReadAsAsync<ApiResponse<BranchDTO>>();
			model = _mapper.Map<BranchVM>(apiResponse.Value);
		}

		return GetPartialView(model, _editBranchModalViewName);
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
			// SetError
			return RedirectToPage($"/Errors/{responseMessage.StatusCode}");
		}

		var apiResponse = await responseMessage.Content.ReadAsAsync<ApiResponse<BranchDTO>>();																		
		var model = _mapper.Map<IReadOnlyCollection<LicenseVM>>(apiResponse.Value.Licenses);

		return GetPartialView(model, _designersPartialViewName);
	}
}
