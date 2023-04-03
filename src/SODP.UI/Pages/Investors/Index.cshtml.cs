using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Investors.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Investors;

public sealed class IndexModel : CollectionPageModel
{
	const string _editInvestorModalViewName = "ModalView/_EditInvestorModalView";

	public IndexModel(
		IWebAPIProvider apiProvider, 
		ILogger<IndexModel> logger, 
		IMapper mapper, 
		LanguageTranslatorFactory translatorFactory) 
		: base(apiProvider, logger, mapper, translatorFactory)
	{
		ReturnUrl = "/Investors";
		_endpoint = "investors";
	}


	public IReadOnlyCollection<InvestorDTO> Investors { get; private set; }


	public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
	{
		var endpoint = GetPageUrl(pageNumber, pageSize, searchString);
		var apiResponse = await GetApiResponseAsync<Page<InvestorDTO>>(endpoint);

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

		var response = await apiResponse.Content.ReadAsAsync<ApiResponse<InvestorDTO>>();

		return GetPartialView(_mapper.Map<InvestorVM>(response.Value), _editInvestorModalViewName);
	}


	public async Task<PartialViewResult> OnPostEditInvestorAsync(InvestorVM investor)
	{
		if (!ModelState.IsValid)
		{
			return GetPartialView(investor, _editInvestorModalViewName);
		}

		var content = investor.ToHttpContent();
		var apiResponse = investor.Id == 0
			? await _apiProvider.PostAsync($"{_endpoint}", content)
			: await _apiProvider.PatchAsync($"{_endpoint}/{investor.Id}", content);

		var response = await apiResponse.Content.ReadAsAsync<ApiResponse>();
		if (!response.IsSuccess)
		{
			foreach(var message in response.Errors)
			{
			}
		}
		
		return GetPartialView(investor, _editInvestorModalViewName);
	}
}
