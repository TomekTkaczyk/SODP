using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Investors.ViewModel;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Investors
{
    public class IndexModel : ListPageModel
    {
		public IndexModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, ITranslator translator) : base(apiProvider, logger, mapper, translator)
		{
			ReturnUrl = "/Investors";
			_endpoint = "investors";
		}

		public InvestorsVM InvestorsViewModel { get; set; }

		public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0)
        {
			var url = new StringBuilder();
			url.Append(ReturnUrl);
			url.Append("?currentPage=:&pageSize=");
			url.Append(pageSize);

			InvestorsViewModel = new InvestorsVM
			{
				PageInfo = new PageInfo
				{
					CurrentPage = currentPage,
					ItemsPerPage = pageSize,
					Url = url.ToString()
				},
			};

			InvestorsViewModel = await GetInvestorsAsync(InvestorsViewModel.PageInfo);

			return Page();
		}

		private async Task<InvestorsVM> GetInvestorsAsync(PageInfo pageInfo)
		{
			var result = new InvestorsVM
			{
				Investors = new List<InvestorDTO>()
			};

			var apiResponse = await _apiProvider.GetAsync($"{_endpoint}?currentPage={pageInfo.CurrentPage}&pageSize={pageInfo.ItemsPerPage}");

			if (apiResponse.IsSuccessStatusCode)
			{
				var response = await apiResponse.Content.ReadAsAsync<ServicePageResponse<InvestorDTO>>();
				result.Investors = response.Data.Collection.ToList();
			}

			return result;
		}
	}
}
