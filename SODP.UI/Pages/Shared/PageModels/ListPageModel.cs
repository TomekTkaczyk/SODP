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

namespace SODP.UI.Pages.Shared.PageModels
{

    public abstract class ListPageModel<T> : ListPageModel where T : BaseDTO
    {
        protected ListPageModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory) { }

        protected void SetPageProperty(StringBuilder url, int pageSize, string searchString)
        {
            PageInfo.Url = url.ToString();
            PageInfo.ItemsPerPage = pageSize;
            SearchString = searchString;
        }

        protected string GetUrl(int currentPage, int pageSize, string searchString)
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

            return url.ToString();
        }

        protected async Task<ServicePageResponse<T>> GetApiResponseAsync(string url)
        {
            var apiResponse = await _apiProvider.GetAsync(url);

            return await apiResponse.Content.ReadAsAsync<ServicePageResponse<T>>();
        }


        protected PageInfo GetPageInfo(ServicePageResponse<T> response, string searchString = "")
        {
            var pageInfo = new PageInfo
            {
                TotalItems = response.Data.TotalCount,
                CurrentPage = response.Data.PageNumber,
                ItemsPerPage = response.Data.PageSize,
                Url = $"{ReturnUrl}?currentPage=:&pageSize={response.Data.PageSize}"
            };
            if (!string.IsNullOrEmpty(searchString))
            {
                pageInfo.Url += $"&searchString={searchString}";
            }

            return pageInfo;
        }


        protected async Task<IList<T>> GetCollectionAsync(string url)
        {

            var apiResponse = await _apiProvider.GetAsync(url);

            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await apiResponse.Content.ReadAsAsync<ServicePageResponse<T>>();
                //PageInfo.TotalItems = response.Data.TotalCount;
                //PageInfo.CurrentPage = response.Data.PageNumber;
                //PageInfo.ItemsPerPage = response.Data.PageSize;
                //PageInfo.Url = GetUrl(ReturnUrl,response.Data.PageSize,response.Data.;

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

        protected ListPageModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(logger, mapper, translatorFactory)
        {
            _apiProvider = apiProvider;
            foreach (var item in PageSizeSelectList.PageSizeList)
            {
                PageSizeList.Add(new SelectListItem(item.ToString(), item.ToString()));
            }
        }
    }
}
