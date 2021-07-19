using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Users
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : SODPPageModel
    {
        private readonly string _apiUrl;
        private readonly string _apiVersion;

        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/Users";
            _apiUrl = apiProvider.apiUrl;
            _apiVersion = apiProvider.apiVersion;
        }
        public ServicePageResponse<UserDTO> Users { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var response = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/users");
            if (response.IsSuccessStatusCode)
            {
                Users = await response.Content.ReadAsAsync<ServicePageResponse<UserDTO>>();
            }

            return Page();
        }
    }
}
