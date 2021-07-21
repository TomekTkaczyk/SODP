using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using SODP.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Designers
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {
        const string partialViewName = "_NewDesignerPartialView";
        private readonly string _apiUrl;
        private readonly string _apiVersion;

        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/designers";
            _apiUrl = apiProvider.apiUrl;
            _apiVersion = apiProvider.apiVersion;
        }
        public DesignersListVM DesignersViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            DesignersViewModel = new DesignersListVM
            {
                Designers = await GetDesignersAsync()
            };

            return Page();
        }

        public PartialViewResult OnGetNewDesigner()
        {
            return GetPartialView(new DesignerDTO());
        }

        public void OnPostNewDesignerAsync()
        {

        }

        private PartialViewResult GetPartialView(DesignerDTO designer)
        {
            var viewModel = new NewDesignerVM()
            {
                Designer = designer
            };

            return new PartialViewResult()
            {
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<NewDesignerVM>(ViewData, viewModel)
            };
        }

        private async Task<IList<DesignerDTO>> GetDesignersAsync()
        {
            var apiResponse = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/designers");
            
            if (apiResponse.IsSuccessStatusCode)
            {
                var result = await apiResponse.Content.ReadAsAsync<ServicePageResponse<DesignerDTO>>();
                return result.Data.Collection.ToList();
            }

            return new List<DesignerDTO>();
        }
    }
}
