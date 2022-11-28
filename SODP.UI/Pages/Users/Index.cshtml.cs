using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Users
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : SODPPageModel
    {
        private readonly IWebAPIProvider _apiProvider;

        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, ITranslator translator) : base(logger, mapper, translator)
        {
            ReturnUrl = "/Users";
            _apiProvider = apiProvider;
        }
        public ServicePageResponse<UserDTO> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _apiProvider.GetAsync($"users");
            if (response.IsSuccessStatusCode)
            {
                Users = await response.Content.ReadAsAsync<ServicePageResponse<UserDTO>>();
            }

            return Page();
        }
    }
}
