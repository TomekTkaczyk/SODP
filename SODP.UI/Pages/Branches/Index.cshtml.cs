using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Mappers;
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
            return GetPartialView(new NewBranchVM());
        }

        public async Task<PartialViewResult> OnPostNewBranchAsync(NewBranchVM branch)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PostAsync($"/branches", branch.ToHttpContent()); 
                
                var response = await _apiProvider.GetContent<ServiceResponse<BranchDTO>>(apiResponse);
                
                if (apiResponse.IsSuccessStatusCode && response.Success)
                {
                    return GetPartialView(new NewBranchVM 
                    {
                        Sign = branch.Sign,
                        Title = branch.Title
                    });
                }
                else
                {
                    SetModelErrors(response);
                }
            }

            return GetPartialView(branch);
        }

        private PartialViewResult GetPartialView(NewBranchVM branch)
        {
            return new PartialViewResult()
            {
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<NewBranchVM>(ViewData, branch)
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
