using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.Response;
using SODP.UI.Api;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Investors.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Investors;

public sealed class IndexModel : CollectionPageModel
{
	const string _editInvestorModalViewName = "ModalView/_EditInvestorModalView";

	public IndexModel(
		IWebAPIProvider apiProvider, 
		ILogger<IndexModel> logger, 
		LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory)
	{
		ReturnUrl = "/Investors";
		_endpoint = "investors";
	}


	public ICollection<InvestorVM> Investors { get; private set; }


	public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber = 1, int pageSize = 0)
	{
		var endpoint = GetPageUrl(searchString, pageNumber, pageSize);
		var apiResponse = await GetApiResponseAsync<Page<InvestorVM>>(endpoint);

		Investors = GetCollection(apiResponse);
		PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}


	public async Task<PartialViewResult> OnGetEditInvestorAsync(int? id)
	{
		if (!id.HasValue)
		{
			return GetPartialView(new InvestorVM(), _editInvestorModalViewName);
		}

		var endpoint = $"{_endpoint}/{id}";
		var apiResponse = await _apiProvider.GetAsync(endpoint);

		if (!apiResponse.IsSuccessStatusCode)
		{
			RedirectToPage($"Errors/{apiResponse.StatusCode}");
		}

		var response = await apiResponse.Content.ReadAsAsync<ApiResponse<InvestorVM>>();

		return GetPartialView(response.Value, _editInvestorModalViewName);
	}


	public async Task<PartialViewResult> OnPostEditInvestorAsync(InvestorVM model)
	{
		if (ModelState.IsValid)
		{
			var responseMessage = model.Id == 0
				? await _apiProvider.PostAsync($"{_endpoint}", model.ToHttpContent())
				: await _apiProvider.PatchAsync($"{_endpoint}/{model.Id}", model.ToHttpContent());
			if (!responseMessage.IsSuccessStatusCode)
			{
				// SetError
			}
		}
		
		return GetPartialView(model, _editInvestorModalViewName);
	}
}
