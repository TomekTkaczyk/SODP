using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        const string licensesPartialViewName = "_LicensesPartialView";
        const string designerPartialViewName = "_EditDesignerPartialView";
        const string licensePartialViewName = "_NewLicensePartialView";

        private readonly IWebAPIProvider _apiProvider;

        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/Designers";
            _apiProvider = apiProvider;
        }

        public DesignersVM Designers { get; set; }

        public LicensesVM Licenses { get; set; }

        [BindProperty]
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

        public async Task<PartialViewResult> OnGetEditDesignerAsync(int? id)
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

        public async Task<PartialViewResult> OnPostEditDesignerAsync(DesignerVM designer)
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

        public async Task<PartialViewResult> OnGetLicensesPartialAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"/designers/{id}/licenses");
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await _apiProvider.GetContent<ServicePageResponse<LicenseWithBranchesDTO>>(apiResponse);
                Licenses = new LicensesVM
                {
                    DesignerId = id,
                    Licenses = response.Data.Collection.Select(x => x.ToViewModel()).ToList(),
                };
            }

            return GetPartialView<LicensesVM>(Licenses, licensesPartialViewName);
        }

        public PartialViewResult OnGetNewLicenseAsync(int designerId)
        {
            var license = new NewLicenseVM
            {
                DesignerId = designerId,
            };

            return GetPartialView<NewLicenseVM>(license, licensePartialViewName);
        }

        public async Task<PartialViewResult> OnPostNewLicenseAsync(NewLicenseVM license)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PostAsync($"/designers/{license.DesignerId}/licences", license.ToHttpContent());
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var response = await _apiProvider.GetContent<ServiceResponse<LicenseDTO>>(apiResponse);
                        if (response.Success)
                        {
                            
                        }
                        SetModelErrors(response);
                        break;
                    default:
                        // redirect to ErrorPage
                        break;
                }
            }

            return GetPartialView<NewLicenseVM>(license, licensePartialViewName);
        }

        public async Task<PartialViewResult> OnPostAddBranchAsync(LicenseVM license)
        {
            await Task.Run(() => Task.Delay(100));
            var pageVM = new LicenseVM
            {
                Branches = new List<SelectListItem>(),
                ApplyBranches = new List<SelectListItem>()
            };
            pageVM.Branches.Add(new SelectListItem {Value="1",Text="blkwjelfkje"});
            pageVM.Branches.Add(new SelectListItem {Value="2",Text="slkjhglwerhg"});

            pageVM.ApplyBranches.Add(new SelectListItem { Value = "3", Text = "fjs;fgsf;gjd;fg" });
            pageVM.ApplyBranches.Add(new SelectListItem { Value = "4", Text = "gefherherh" });
            pageVM.ApplyBranches.Add(new SelectListItem { Value = "5", Text = "fgnfgfqgrergwererg" });

            var page = GetPartialView<LicenseVM>(pageVM, licensePartialViewName);

            return page;
        }

        public async Task<PartialViewResult> OnGetRemoveBranchAsync(int id, int branchId)
        {
            LicenseVM license;
            var apiResponse = await _apiProvider.GetAsync($"/licenses/{id}/branches");
            var response = await _apiProvider.GetContent<ServiceResponse<LicenseWithBranchesDTO>>(apiResponse);

            license = response.Data.ToViewModel();
            license.Branches = await GetBranchesAsync(license.ApplyBranches);

            return GetPartialView(license, licensePartialViewName);
        }

        private async Task<List<DesignerVM>> GetDesignersAsync(PageInfo pageInfo)
        {
            var apiResponse = await _apiProvider.GetAsync($"/designers?currentPage={pageInfo.CurrentPage}&pageSize={pageInfo.ItemsPerPage}");
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await _apiProvider.GetContent<ServicePageResponse<DesignerDTO>>(apiResponse);
                //pageInfo.TotalItems = response.Data.TotalCount;
                //pageInfo.CurrentPage = response.Data.PageNumber;

                return response.Data.Collection
                    .Select(x => new DesignerVM 
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Firstname = x.Firstname,
                        Lastname = x.Lastname,
                        ActiveStatus = x.ActiveStatus
                    }).ToList();
            }

            return new List<DesignerVM>();
        }

        private async Task<List<SelectListItem>> GetBranchesAsync() => await GetBranchesAsync(null);

        private async Task<List<SelectListItem>> GetBranchesAsync(List<SelectListItem> exclusionList)
        {
            var apiResponse = await _apiProvider.GetAsync($"/branches");
            var responseBranch = await _apiProvider.GetContent<ServicePageResponse<BranchDTO>>(apiResponse);
            var result = responseBranch.Data.Collection
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                }).ToList();

            if (exclusionList != null)
            {
                var shortList = result.Where(y => !exclusionList.Contains(y,new SelectListItemComparer())).ToList();
                result = shortList;
            }

            return result;
        }
    }
}
