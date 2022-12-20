using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Services;
using SODP.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared
{
    public abstract class ProjectsPageModel : ListPageModel
    {

        public ProjectsListVM ProjectsViewModel { get; set; }

        protected ProjectsPageModel(IWebAPIProvider apiProvider, ILogger<ProjectsPageModel> logger, IMapper mapper, ITranslator translator) : base(apiProvider, logger, mapper, translator) { }

        protected async Task<IActionResult> OnGetAsync(ProjectStatus status, int currentPage = 1, int pageSize = 0, string searchString = "")
        {
			var url = new StringBuilder();
			url.Append(ReturnUrl);
			url.Append("?currentPage=:&pageSize=");
			pageSize = pageSize < 1 ? PageSizeSelectList.PageSizeList[0] : pageSize;
			url.Append(pageSize);

			if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
			{
				url.Append($"&searchString={searchString}");
			}

			ProjectsViewModel = new ProjectsListVM
            {
                Projects = await GetProjectsAsync(status, currentPage, pageSize, searchString)
            };

			SearchString = searchString;
			PageInfo.ItemsPerPage = pageSize;
			PageInfo.Url = url.ToString();

			return Page();
        }

        private async Task<IList<ProjectDTO>> GetProjectsAsync(ProjectStatus status, int currentPage, int pageSize, string searchString)
        {
            var url = new StringBuilder();
            url.Append(_endpoint);
            url.Append($"?status={status}");
            url.Append($"&currentPage={currentPage}");
            pageSize = pageSize < 1 ? PageSizeSelectList.PageSizeList[0] : pageSize;
            url.Append($"&pageSize={pageSize}");
            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
            {
                url.Append($"&searchString={searchString}");
            }

            var apiResponse = await _apiProvider.GetAsync(url.ToString());

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
