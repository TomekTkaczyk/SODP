using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
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
    public class IndexModel : ListPageModel
    {
        const string editDesignerPartialViewName = "_EditDesignerPartialView";
        const string licensesPartialViewName = "_LicensesPartialView";
        const string newLicensePartialViewName = "_NewLicensePartialView";

        public IndexModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger) : base(apiProvider, logger)
        {
            ReturnUrl = "/Designers";
            _endpoint = "designers";
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

        public async Task<PartialViewResult> OnGetEditDesignerAsync(int? id)
        {
            if (id != null)
            {
                var apiResponse = await _apiProvider.GetAsync($"designers/{id}");
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<DesignerDTO>>();

                    return GetPartialView(response.Data.ToViewModel(), editDesignerPartialViewName);
                }
            }

            return GetPartialView(new DesignerVM(), editDesignerPartialViewName);
        }

        public async Task<PartialViewResult> OnPostEditDesignerAsync(DesignerVM designer)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = designer.Id == 0
                    ? await _apiProvider.PostAsync($"designers", designer.ToHttpContent())
                    : await _apiProvider.PutAsync($"designers/{designer.Id}", designer.ToHttpContent());
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

            return GetPartialView(designer, editDesignerPartialViewName);
        }

        public async Task<PartialViewResult> OnGetLicensesPartialAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"designers/{id}/licenses");
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

            return GetPartialView(license, newLicensePartialViewName);
        }

        public async Task<PartialViewResult> OnPostNewLicenseAsync(NewLicenseVM license)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PostAsync($"designers/{license.DesignerId}/licences", license.ToHttpContent());
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

            return GetPartialView<NewLicenseVM>(license, newLicensePartialViewName);
        }

        private async Task<List<DesignerVM>> GetDesignersAsync(PageInfo pageInfo)
        {
            var apiResponse = await _apiProvider.GetAsync($"{_endpoint}?currentPage={pageInfo.CurrentPage}&pageSize={pageInfo.ItemsPerPage}");
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
    }
}
