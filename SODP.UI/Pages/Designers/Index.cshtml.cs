using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Designers.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System;
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
        const string _newLicenseModalViewName = "ModalView/_NewLicenseModalView";
        const string _editDesignerModalViewName = "ModalView/_EditDesignerModalView";
        const string _licensesPartialViewName = "PartialView/_LicensesPartialView";

        public IndexModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
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
			var endpoint = GetUrl(currentPage, pageSize, searchString);
			var apiResponse = await GetApiResponseAsync(endpoint);
			
            PageInfo = GetPageInfo(apiResponse, searchString);
            Designers = new DesignersVM
            {
                Designers = apiResponse.Data.Collection.ToList(),
                PageInfo = PageInfo
			};

            return Page();
        }

        public async Task<IActionResult> OnGetEditDesignerAsync(int? id)
        {
			var model = new DesignerVM();
			if (id != null)
			{
				var apiResponse = await _apiProvider.GetAsync($"designers/{id}");
				if (!apiResponse.IsSuccessStatusCode)
				{
					RedirectToPage("Errors/404");
				}
				var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<DesignerDTO>>();
				model = response.Data.ToViewModel();
			}

			return GetPartialView(model, _editDesignerModalViewName);
		}

        public async Task<IActionResult> OnPostEditDesignerAsync(DesignerVM designer)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = designer.Id == 0
                    ? await _apiProvider.PostAsync($"{_endpoint}", designer.ToHttpContent())
                    : await _apiProvider.PutAsync($"{_endpoint}/{designer.Id}", designer.ToHttpContent());
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

            return GetPartialView(designer, _editDesignerModalViewName);
        }

        public async Task<IActionResult> OnGetLicensesPartialAsync(int id)
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

            return GetPartialView(Licenses, _licensesPartialViewName);
        }

        public IActionResult OnGetNewLicense(int id)
        {
			var model = new NewLicenseVM
			{
				DesignerId = id,
				Content = ""
			};

			return GetPartialView(model, _newLicenseModalViewName);
		}

		public async Task<IActionResult> OnPostNewLicenseAsync(NewLicenseVM license)
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

            return GetPartialView<NewLicenseVM>(license, _newLicenseModalViewName);
        }
    }
}
