using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Branches
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {
        private readonly string _apiUrl;
        private readonly string _apiVersion;
        
        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/Branches";
            _apiUrl = apiProvider.apiUrl;
            _apiVersion = apiProvider.apiVersion;
        }

        public ServicePageResponse<BranchDTO> Branches { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/branches");
            if (response.IsSuccessStatusCode)
            {
                Branches = await response.Content.ReadAsAsync<ServicePageResponse<BranchDTO>>();
            }

            return Page();
        }
    }
}
