using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ArchiveProjects
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {
        private readonly string _apiUrl;
        private readonly string _apiVersion;

        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/ArchiveProjects";
            _apiUrl = apiProvider.apiUrl;
            _apiVersion = apiProvider.apiVersion;
        }
        public ServicePageResponse<ProjectDTO> Projects { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/archive-projects");
            if (response.IsSuccessStatusCode)
            {
                Projects = await response.Content.ReadAsAsync<ServicePageResponse<ProjectDTO>>();
            }

            return Page();
        }
    }
}
