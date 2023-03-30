using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Investors.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Stages.ViewModels;
using SODP.UI.Services;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Investors;

public class IndexModel : ListPageModel<InvestorDTO>
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


	public InvestorsVM Investors { get; } = new();


	public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
	{
		var endpoint = GetPageUrl(pageNumber, pageSize, searchString);
		var apiResponse = await GetApiResponseAsync(endpoint);

		Investors.Investors = apiResponse.Data.Collection.ToList();
		Investors.PageInfo = GetPageInfo(apiResponse, searchString);

		return Page();
	}


	public async Task<PartialViewResult> OnGetEditInvestorAsync(int? id)
	{
		var model = new InvestorVM();
		if (!id.HasValue)
		{
			return GetPartialView(model, _editInvestorModalViewName);
		}

		var endpoint = $"{_endpoint}/{id}";
		var apiResponse = await _apiProvider.GetAsync(endpoint);
		if (!apiResponse.IsSuccessStatusCode)
		{
			RedirectToPage($"Errors/{(int)apiResponse.StatusCode}");
		}

		var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<InvestorDTO>>();
		try
		{
			model = _mapper.Map<InvestorVM>(result.Data);
		}
		catch (System.Exception ex)
		{
			throw;
		}

		return GetPartialView(model, _editInvestorModalViewName);
	}


	public async Task<PartialViewResult> OnPostEditInvestorAsync(InvestorVM investor)
	{
		if (ModelState.IsValid)
		{
			var apiResponse = investor.Id == 0
				? await _apiProvider.PostAsync($"{_endpoint}", investor.ToHttpContent())
				: await _apiProvider.PutAsync($"{_endpoint}/{investor.Id}", investor.ToHttpContent());
			switch (apiResponse.StatusCode)
			{
				case HttpStatusCode.OK:
					var response = await _apiProvider.GetContent<ServiceResponse<InvestorDTO>>(apiResponse);
					if (!response.Success)
					{
						SetModelErrors(response);
					}
					break;
				default:
					// redirect to ErrorPage
					break;
			}
		}
		return GetPartialView(investor, _editInvestorModalViewName);

	}
}
