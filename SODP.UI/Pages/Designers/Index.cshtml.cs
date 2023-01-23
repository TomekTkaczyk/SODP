using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Designers.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Designers
{
    [Authorize(Roles = "ProjectManager")]
	public class IndexModel : ListPageModel<DesignerDTO>
    {
        const string editDesignerPartialViewName = "_EditDesignerPartialView";
        const string licensesPartialViewName = "_LicensesPartialView";
        const string newLicensePartialViewName = "_NewLicensePartialView";

        public IndexModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, ITranslator translator) : base(apiProvider, logger, mapper, translator)
        {
            ReturnUrl = "/Designers";
            _endpoint = "designers";
        }

        public DesignersVM Designers { get; set; }

        public LicensesVM Licenses { get; set; }

        public LicenseVM License { get; set; }

        public BranchesVM Branches { get; set; }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            Designers = new DesignersVM
            {
                Designers = await GetCollectionAsync(currentPage, pageSize, searchString)
            };
			SearchString = searchString;

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

				RedirectToPage("Errors/404");
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

            return GetPartialView(Licenses, licensesPartialViewName);
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
                var apiResponse = await _apiProvider.PostAsync($"designers/{license.DesignerId}/licenses", license.ToHttpContent());
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
    }
}
