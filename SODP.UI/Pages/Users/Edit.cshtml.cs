using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.PageModels;
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

        public EditModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(logger, mapper, translatorFactory)
        {
            ReturnUrl = "/Users/Edit";
			_apiProvider = apiProvider;
        }

        [BindProperty]
        public UserDTO CurrentUser { get; set; }

        [BindProperty]
        public IDictionary<string,bool> AllRoles { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var roles = await GetRoles();
            AllRoles = roles.Data.Collection.ToDictionary(x => x.Role, x => false);
            CurrentUser = await GetUser(id);
            if(CurrentUser == null)
            {
                return Redirect("/Errors/404");
            }

            return Page();
        }

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				CurrentUser.Roles = AllRoles.Where(x => x.Value).Select(x => x.Key).ToList();
				var apiResponse = await _apiProvider.PutAsync($"users/{CurrentUser.Id}",
					new StringContent(
						JsonSerializer.Serialize(CurrentUser),
						Encoding.UTF8,
						"application/json"
						));
				if (apiResponse.IsSuccessStatusCode)
				{
					return RedirectToPage("Index");
				}
			}

			return Page();
		}

		private async Task<UserDTO> GetUser(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"users/{id}");
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
            var apiResponse = await _apiProvider.GetAsync($"roles");
            if (apiResponse.IsSuccessStatusCode)
            {
                var result = await apiResponse.Content.ReadAsAsync<ServicePageResponse<RoleDTO>>();
                return result;
            }

            return null;
        }

    }
}
