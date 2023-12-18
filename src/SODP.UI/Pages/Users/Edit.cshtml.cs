using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Users.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Users
{
	[Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken()]
    public class EditModel : SODPPageModel
	{
        private readonly IWebAPIProvider _apiProvider;

        public EditModel(
            IWebAPIProvider apiProvider, 
            ILogger<EditModel> logger, 
            IMapper mapper, 
            LanguageTranslatorFactory translatorFactory) : base(logger, mapper, translatorFactory)
        {
            ReturnUrl = "/Users/Edit";
			_apiProvider = apiProvider;
        }

        [BindProperty]
        public UserVM CurrentUser { get; set; }

        [BindProperty]
        public IDictionary<string,bool> AllRoles { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var roles = await GetRolesAsync();
            AllRoles = roles.Value.Collection.ToDictionary(x => x.Role, x => false);
            CurrentUser = await GetUserAsync(id);
            if(CurrentUser == null)
            {
                return Redirect("/Errors/404");
            }

            return Page();
        }

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				CurrentUser.Roles = AllRoles.Where(x => x.Value).Select(x => x.Key).ToList();
                var apiResponse = await _apiProvider.PutAsync($"users/{CurrentUser.Id}", CurrentUser.ToHttpContent());
				if (apiResponse.IsSuccessStatusCode)
				{
					return RedirectToPage("Index");
				}
			}

			return Page();
		}

		private async Task<UserVM> GetUserAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"users/{id}");
            if (apiResponse.IsSuccessStatusCode) 
            { 
                var response = await apiResponse.Content.ReadAsAsync<ApiResponse<UserVM>>();
                if (response.IsSuccess)
                {
                    return response.Value;
                }
            }

            return null;
        }

        private async Task<ApiResponse<Page<RoleVM>>> GetRolesAsync()
        {
            var apiResponse = await _apiProvider.GetAsync($"roles");
            if (apiResponse.IsSuccessStatusCode)
            {
                var result = await apiResponse.Content.ReadAsAsync<ApiResponse<Page<RoleVM>>>();
                return result;
            }

            return null;
        }

    }
}
