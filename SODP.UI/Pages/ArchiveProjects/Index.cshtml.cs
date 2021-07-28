using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using SODP.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ArchiveProjects
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {
        private readonly IWebAPIProvider _apiProvider;

        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/ArchiveProjects";
            _apiProvider = apiProvider;
        }
        public ProjectsListVM ProjectsViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 15)
        {
            var url = new StringBuilder();
            url.Append(ReturnUrl);
            url.Append("?currentPage=:&pageSize=");
            url.Append(pageSize.ToString());

            ProjectsViewModel = new ProjectsListVM
            {
                PageInfo = new PageInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = pageSize,
                    Url = url.ToString()
                },
            };

            ProjectsViewModel.Projects = await GetProjectsAsync(ProjectsViewModel.PageInfo);

            return Page();
        }


        //public async Task<IActionResult> OnGetAsync()
        //{
        //    var response = await _apiProvider.GetAsync($"/archive-projects");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        ProjectsViewModel = await response.Content.ReadAsAsync<ServicePageResponse<ProjectDTO>>();
        //    }

        //    return Page();
        //}

        private async Task<IList<ProjectDTO>> GetProjectsAsync(PageInfo pageInfo)
        {
            var apiResponse = await _apiProvider.GetAsync($"/archive-projects?currentPage={pageInfo.CurrentPage}&pageSize={pageInfo.ItemsPerPage}");
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await apiResponse.Content.ReadAsAsync<ServicePageResponse<ProjectDTO>>();
                pageInfo.TotalItems = response.Data.TotalCount;
                pageInfo.CurrentPage = response.Data.PageNumber;

                return response.Data.Collection.ToList();
            }

            return new List<ProjectDTO>();
        }

    }
}
