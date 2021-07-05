using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Designers
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
        public ServicePageResponse<DesignerDTO> Designers { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/designers");
            if (response.IsSuccessStatusCode)
            {
                Designers = await response.Content.ReadAsAsync<ServicePageResponse<DesignerDTO>>();
            }

            return Page();
        }

        public PartialViewResult OnGetDesignerDetails()
        {
            var partialViewResult = new PartialViewResult
            {
                ViewName = "_DesignerPartialView",
                ViewData = new ViewDataDictionary<DesignerDTO>(ViewData, new DesignerDTO())
            };

            return partialViewResult;
        }

        public void OnPostDesignerDetails()
        {

        }
    }
}
