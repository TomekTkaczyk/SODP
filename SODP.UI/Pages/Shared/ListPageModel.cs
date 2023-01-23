using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared
{

	public abstract class ListPageModel<T> : ListPageModel where T : BaseDTO
	{
		protected ListPageModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, ITranslator translator) : base(apiProvider, logger, mapper, translator) { }

		protected async Task<IList<T>> GetCollectionAsync(int currentPage, int pageSize, string searchString)
		{
			var url = new StringBuilder();
			url.Append(_endpoint);
			url.Append($"?currentPage={currentPage}");
			pageSize = pageSize < 1 ? PageSizeSelectList.PageSizeList[0] : pageSize;
			url.Append($"&pageSize={pageSize}");
			if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
			{
				url.Append($"&searchString={searchString}");
			}

			var apiResponse = await _apiProvider.GetAsync(url.ToString());

			if (apiResponse.IsSuccessStatusCode)
			{
				var response = await apiResponse.Content.ReadAsAsync<ServicePageResponse<T>>();
				PageInfo.TotalItems = response.Data.TotalCount;
				PageInfo.CurrentPage = response.Data.PageNumber;
				PageInfo.ItemsPerPage = pageSize;
				PageInfo.Url= url.ToString();

				return response.Data.Collection.ToList();
			}

			return new List<T>();
		}
	}

	public abstract class ListPageModel : SODPPageModel
    {
        protected readonly IWebAPIProvider _apiProvider;
        protected string _endpoint;

        public string SearchString { get; set; }
        
        public PageInfo PageInfo { get; set; } = new PageInfo();

        public List<SelectListItem> PageSizeList { get; set; } = new List<SelectListItem> { };

        public int PageSize { get; set; }

        protected ListPageModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, ITranslator translator) : base(logger, mapper, translator) 
        { 
            _apiProvider = apiProvider;
            foreach (var item in PageSizeSelectList.PageSizeList)
            {
                PageSizeList.Add(new SelectListItem(item.ToString(), item.ToString()));
            }
        }
	}
}
