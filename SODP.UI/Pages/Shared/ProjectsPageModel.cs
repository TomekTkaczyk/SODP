using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using SODP.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared
{
    public class ProjectsPageModel : ListPageModel
    {

        public ProjectsListVM ProjectsViewModel { get; set; }

        public ProjectsPageModel(IWebAPIProvider apiProvider, ILogger<ProjectsPageModel> logger, IMapper mapper, ITranslator translator) : base(apiProvider, logger, mapper, translator) { }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            var url = new StringBuilder();
            url.Append(ReturnUrl);
            url.Append("?currentPage=:&pageSize=");
            pageSize = pageSize < 1 ? PageSizeSelectList.PageSizeList[0] : pageSize;
            url.Append(pageSize.ToString());

            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
            {
                url.Append($"&searchString={searchString}");
            }

            ProjectsViewModel = new ProjectsListVM
            {
                Projects = await GetProjectsAsync(currentPage, pageSize, searchString)
            };

            SearchString = searchString;
            PageInfo.ItemsPerPage = pageSize;
            PageInfo.Url = url.ToString();

            return Page();
        }

        protected async Task<IList<ProjectDTO>> GetProjectsAsync(int currentPage, int pageSize, string searchString)
        {
            var url = $"{_endpoint}?currentPage={currentPage}&pageSize={pageSize}&searchString={searchString}";
            var apiResponse = await _apiProvider.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await apiResponse.Content.ReadAsAsync<ServicePageResponse<ProjectDTO>>();
                PageInfo.TotalItems = response.Data.TotalCount;
                PageInfo.CurrentPage = response.Data.PageNumber;

                return response.Data.Collection.ToList();
            }

            return new List<ProjectDTO>();
        }
    }
}
