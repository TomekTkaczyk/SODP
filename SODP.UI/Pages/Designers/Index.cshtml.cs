using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Designers.ViewModels;
using SODP.UI.Pages.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Designers
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {
        const string designerPartialViewName = "_NewDesignerPartialView";
        const string licensePartialViewName = "_NewLicensePartialView";
        const string licensesPartialViewName = "_LicensesPartialView";
        const string branchesPartialViewName = "_SelectBranchPartialView";

        private readonly IWebAPIProvider _apiProvider;

        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/Designers";
            _apiProvider = apiProvider;
        }

        public DesignersVM Designers { get; set; }

        public LicensesVM Licenses { get; set; }

        public LicenseVM License { get; set; }

        public BranchesVM Branches { get; set; }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0)
        {
            var url = new StringBuilder();
            url.Append(ReturnUrl);
            url.Append("?currentPage=:&pageSize=");
            url.Append(pageSize.ToString());

            Designers = new DesignersVM
            {
                PageInfo = new PageInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = pageSize,
                    Url = url.ToString()
                },
            };

            Designers.Designers = await GetDesignersAsync(Designers.PageInfo);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _apiProvider.DeleteAsync($"/designers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Page();
            }

            return RedirectToPage($"Index?currentPage={Designers.PageInfo.CurrentPage}:&pageSize={Designers.PageInfo.ItemsPerPage}");
        }

        public async Task<PartialViewResult> OnGetNewDesignerAsync(int? id)
        {
            if (id != null)
            {
                var apiResponse = await _apiProvider.GetAsync($"/designers/{id}");
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<DesignerDTO>>();

                    return GetPartialView(response.Data.ToViewModel(), designerPartialViewName);
                }
            }

            return GetPartialView(new DesignerVM(), designerPartialViewName);
        }

        public async Task<PartialViewResult> OnPostNewDesignerAsync(DesignerVM designer)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = designer.Id == 0
                    ? await _apiProvider.PostAsync($"/designers", designer.ToHttpContent())
                    : await _apiProvider.PutAsync($"/designers/{designer.Id}", designer.ToHttpContent());
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var response = await _apiProvider.GetContent<ServiceResponse<DesignerDTO>>(apiResponse);
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

            return GetPartialView<DesignerVM>(designer, designerPartialViewName);
        }

        public async Task<PartialViewResult> OnGetPartialLicenses(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"/designers/{id}/licenses");
            var response = await _apiProvider.GetContent<ServicePageResponse<LicenseWithBranchesDTO>>(apiResponse);
            if (apiResponse.IsSuccessStatusCode)
            {
                Licenses = new LicensesVM
                {   
                    DesignerId = id,
                    Licenses = response.Data.Collection.ToList(),
                };
            }

            return GetPartialView<LicensesVM>(Licenses, licensesPartialViewName);
        }

        public async Task<PartialViewResult> OnGetSelectBranchAsync(int id)
        {
            var branches = await GetBranchesVM(new BranchesVM { LicenseId = id });

            return GetPartialView<BranchesVM>(branches, branchesPartialViewName);
        }



        public async Task<PartialViewResult> OnPostSelectBranchAsync(BranchesVM branches)
        {
            await _apiProvider.PostAsync($"/licenses/{branches.LicenseId}/branch/{branches.BranchId}", branches.ToHttpContent());

            branches = await GetBranchesVM(branches);

            return GetPartialView<BranchesVM>(branches, branchesPartialViewName);
        }
 
        public async Task<PartialViewResult> OnGetNewLicenseAsync(int designerId, int? id)
        {
            if (id != null)
            {
                var apiResponse = await _apiProvider.GetAsync($"/licenses/{id}");
                var response = await _apiProvider.GetContent<ServiceResponse<LicenseDTO>>(apiResponse);
                if (apiResponse.IsSuccessStatusCode)
                {
                    return GetPartialView(response.Data.ToViewModel(), licensePartialViewName);
                }
            }

            return GetPartialView<LicenseVM>(new LicenseVM { DesignerId = designerId }, licensePartialViewName);
        }


        public async Task<PartialViewResult> OnPostNewLicenseAsync(LicenseVM license)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = license.Id == 0
                    ? await _apiProvider.PostAsync($"/designers/{license.DesignerId}/licences", license.ToHttpContent())
                    : await _apiProvider.PutAsync($"/licenses/{license.Id}", license.ToHttpContent());
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var response = await _apiProvider.GetContent<ServiceResponse<LicenseDTO>>(apiResponse);
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

            return GetPartialView<LicenseVM>(license, licensePartialViewName);
        }

        private async Task<IList<DesignerDTO>> GetDesignersAsync(PageInfo pageInfo)
        {
            var apiResponse = await _apiProvider.GetAsync($"/designers?currentPage={pageInfo.CurrentPage}&pageSize={pageInfo.ItemsPerPage}");
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await _apiProvider.GetContent<ServicePageResponse<DesignerDTO>>(apiResponse);
                pageInfo.TotalItems = response.Data.TotalCount;
                pageInfo.CurrentPage = response.Data.PageNumber;
                
                return response.Data.Collection.ToList();
            }

            return new List<DesignerDTO>();
        }

        private async Task<PartialViewResult> GetPartialViewAsync(BranchesVM branches)
        {
            var apiResponse = await _apiProvider.GetAsync($"/licenses/{branches.LicenseId}/branches");
            var response = await _apiProvider.GetContent<ServiceResponse<LicenseWithBranchesDTO>>(apiResponse);

            apiResponse = await _apiProvider.GetAsync($"/branches");
            var responseBranch = await _apiProvider.GetContent<ServicePageResponse<BranchDTO>>(apiResponse);

            var branchesItems = responseBranch.Data.Collection
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                }).ToList();

            branches.Branches = branchesItems;

            return new PartialViewResult
            {
                ViewName = branchesPartialViewName,
                ViewData = new ViewDataDictionary<BranchesVM>(ViewData, branches)
            };
        }

        private async Task<BranchesVM> GetBranchesVM(BranchesVM branches)
        {
            var apiResponse = await _apiProvider.GetAsync($"/licenses/{branches.LicenseId}/branches");
            var response = await _apiProvider.GetContent<ServiceResponse<LicenseWithBranchesDTO>>(apiResponse);

            apiResponse = await _apiProvider.GetAsync($"/branches");
            var responseBranch = await _apiProvider.GetContent<ServicePageResponse<BranchDTO>>(apiResponse);

            var branchesItems = responseBranch.Data.Collection
                .Where(y => response.Data.Branches.FirstOrDefault(z => z.Sign == y.Sign) == null)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                }).ToList();

            branches.Branches = branchesItems;

            return branches;
        }
    }
}
