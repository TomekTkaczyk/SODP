using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Users
{
    // [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken()]
    public class EditModel : PageModel
    {
        private readonly string _apiUrl;
        private readonly string _apiVersion;

        public EditModel(IWebAPIProvider apiProvider)
        {
            _apiUrl = apiProvider.apiUrl;
            _apiVersion = apiProvider.apiVersion;
        }

        [BindProperty]
        public UserDTO CurrentUser { get; set; }

        [BindProperty]
        public IDictionary<string,bool> AllRoles { get; set; }

        public string ReturnUrl { get; } = "/Users";

        public async Task<IActionResult> OnGet(int id)
        {
            var roles = await GetRoles();
            AllRoles = roles.Data.Collection.ToDictionary(x => x.Role, x => false);
            CurrentUser = await GetUser(id);

            return Page();
        }

        private async Task<UserDTO> GetUser(int id)
        {
            var apiResponse = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/users/{id}");
            if (apiResponse.IsSuccessStatusCode) 
            { 
                var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<UserDTO>>();
                if (response.Success)
                {
                    return response.Data;
                }
            }

            return null;
        }

        private async Task<ServicePageResponse<RoleDTO>> GetRoles()
        {
            var apiResponse = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/roles");
            if (apiResponse.IsSuccessStatusCode)
            {
                var result = await apiResponse.Content.ReadAsAsync<ServicePageResponse<RoleDTO>>();
                return result;
            }

            return null;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                CurrentUser.Roles = AllRoles.Where(x => x.Value).Select(x => x.Key).ToList();
                var body = new StringContent(JsonSerializer.Serialize(CurrentUser), Encoding.UTF8, "application/json");
                var apiResponse = await new HttpClient().PutAsync($"{_apiUrl}{_apiVersion}/users/{CurrentUser.Id}",body);
                if (apiResponse.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}
