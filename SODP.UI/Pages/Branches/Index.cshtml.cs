using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Mappers;
using SODP.UI.Pages.Branches.ViewModels;
using SODP.UI.Pages.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Branches
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {
        const string newBranchPartialViewName = "_NewBranchPartialView";
        const string designersPartialViewName = "_DesignersPartialView";
        
        private readonly IWebAPIProvider _apiProvider;

        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger) : base(logger)
        {
            ReturnUrl = "/branches";
            _apiProvider = apiProvider;
        }

        public BranchesVM Branches { get; set; }

        public DesignersVM Designers { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Branches = await GetBranchesAsync();
            return Page();
        }

        public async Task<PartialViewResult> OnGetNewBranchAsync(int? id)
        {
            if (id != null)
            {
                var apiResponse = await _apiProvider.GetAsync($"branches/{id}");
                if (apiResponse.IsSuccessStatusCode)
                {
                    var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<BranchDTO>>();

                    return GetPartialView(result.Data.ToViewModel(), newBranchPartialViewName);
                }
            }

            return GetPartialView(new BranchVM(), newBranchPartialViewName);
        }

        public async Task<PartialViewResult> OnPostNewBranchAsync(BranchVM branch)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = branch.Id == 0 
                    ? await _apiProvider.PostAsync($"branches", branch.ToHttpContent())
                    : await _apiProvider.PutAsync($"branches/{branch.Id}", branch.ToHttpContent());

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var response = await _apiProvider.GetContent<ServiceResponse<BranchDTO>>(apiResponse);
                        if (!response.Success)
                        {
                            SetModelErrors(response);
                        }
                        break;
                    default:
                        // redirect to ErrorPage
                        break;
                }
            }

            return GetPartialView<BranchVM>(branch, newBranchPartialViewName);
        }

        public async Task<PartialViewResult> OnGetPartialDesignersAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"branches/{id}/designers");
            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    var response = await _apiProvider.GetContent<ServicePageResponse<LicenseDTO>>(apiResponse);
                    if (response.Success)
                    {
                        Designers = new DesignersVM
                        {
                            Licenses = response.Data.Collection.ToList()
                        };
                    }
                    break;
                default:
                    // redirect to ErrorPage
                    break;
            }

            return GetPartialView<DesignersVM>(Designers, designersPartialViewName);
        } 

        private async Task<BranchesVM> GetBranchesAsync()
        {
            var result = new BranchesVM
            {
                Branches = new List<BranchDTO>()
            };

            var apiResponse = await _apiProvider.GetAsync($"branches");

            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await apiResponse.Content.ReadAsAsync<ServicePageResponse<BranchDTO>>();
                result.Branches = response.Data.Collection.ToList();
            }

            return result;
        }
    }
}
