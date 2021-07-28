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
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Branches
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {
        const string partialViewName = "_NewBranchPartialView";
        private readonly IWebAPIProvider _apiProvider;

        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/branches";
            _apiProvider = apiProvider;
        }

        public BranchesListVM BranchesViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            BranchesViewModel = new BranchesListVM
            {
                Branches = await GetBranchesAsync()
            };

            return Page();
        }

        public PartialViewResult OnGetNewBranch()
        {
            return GetPartialView(new BranchDTO());
        }

        public async Task<PartialViewResult> OnPostNewBranchAsync(BranchDTO branch)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PostAsync($"/branches", 
                    new StringContent(
                        JsonSerializer.Serialize(branch), 
                        Encoding.UTF8, 
                        "application/json"
                        ));
                var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<BranchDTO>>();
                if (response.Success)
                {
                    branch = response.Data;
                }
                else
                {
                    SetModelErrors(response);
                }
            }

            return GetPartialView(branch);
        }

        private PartialViewResult GetPartialView(BranchDTO branch)
        {
            var viewModel = new NewBranchVM()
            {
                Branch = branch
            };

            return new PartialViewResult()
            {
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<NewBranchVM>(ViewData, viewModel)
            };
        }

        private async Task<IList<BranchDTO>> GetBranchesAsync()
        {
            var apiResponse = await _apiProvider.GetAsync($"/branches");

            if (apiResponse.IsSuccessStatusCode)
            {
                var result = await apiResponse.Content.ReadAsAsync<ServicePageResponse<BranchDTO>>();
                return result.Data.Collection.ToList();
            }

            return new List<BranchDTO>();
        }


    }
}
