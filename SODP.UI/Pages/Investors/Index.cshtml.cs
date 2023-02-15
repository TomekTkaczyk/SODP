using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Designers.ViewModels;
using SODP.UI.Pages.Investors.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Investors
{
	public class IndexModel : ListPageModel<InvestorDTO>
    {
		const string editInvestorPartialViewName = "_EditInvestorPartialView";

		public IndexModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
		{
			ReturnUrl = "/Investors";
			_endpoint = "investors";
		}


		public InvestorsVM Investors { get; set; }


		public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0)
        {
			var url = new StringBuilder();
			url.Append(ReturnUrl);
			url.Append("?currentPage=");
			url.Append(currentPage);
			url.Append("&pageSize=");
			url.Append(pageSize);

			PageInfo.CurrentPage = currentPage;
			PageInfo.ItemsPerPage = pageSize;
			PageInfo.Url = url.ToString();

			Investors = await GetInvestorsAsync();

			return Page();
		}


		public async Task<PartialViewResult> OnGetEditInvestorAsync(int? id)
		{
			if(id != null)
			{
                var apiResponse = await _apiProvider.GetAsync($"investors/{id}");
				if(apiResponse.IsSuccessStatusCode)
				{
					var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<InvestorDTO>>();

					return GetPartialView(response.Data.ToViewModel(), editInvestorPartialViewName);
				}
                RedirectToPage("Errors/404");
            }

            return GetPartialView(new InvestorVM(), editInvestorPartialViewName);
        }


		public async Task<PartialViewResult> OnPostEditInvestorAsync(InvestorVM investor)
		{
            if (ModelState.IsValid)
			{
                var apiResponse = investor.Id == 0
                    ? await _apiProvider.PostAsync($"investors", investor.ToHttpContent())
					: await _apiProvider.PutAsync($"investors/{investor.Id}", investor.ToHttpContent());
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
			return GetPartialView(investor, editInvestorPartialViewName);

        }


        private async Task<InvestorsVM> GetInvestorsAsync()
		{
			var result = new InvestorsVM
			{
				Investors = new List<InvestorDTO>()
			};

			var apiResponse = await _apiProvider.GetAsync($"{_endpoint}?currentPage={PageInfo.CurrentPage}&pageSize={PageInfo.ItemsPerPage}");

			if (apiResponse.IsSuccessStatusCode)
			{
				var response = await apiResponse.Content.ReadAsAsync<ServicePageResponse<InvestorDTO>>();
				result.Investors = response.Data.Collection.ToList();
			}

			return result;
		}
	}
}
