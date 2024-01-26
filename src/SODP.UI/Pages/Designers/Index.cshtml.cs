using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Designers.ViewModels;
using SODP.UI.Pages.Shared.Extensions;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Designers
{
    [Authorize(Roles = "ProjectManager")]
	public class IndexModel : CollectionPageModel
    {
        const string _newLicenseModalViewName = "ModalView/_NewLicenseModalView";
        const string _editDesignerModalViewName = "ModalView/_EditDesignerModalView";
        const string _licensesPartialViewName = "PartialView/_LicensesPartialView";

        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, translatorFactory)
        {
            ReturnUrl = "/Designers";
            _endpoint = "designers";
        }

        public IEnumerable<DesignerVM> Designers { get; set; }

        // public LicensesVM Licenses { get; set; }

        //public LicenseVM License { get; set; }

        //public BranchesVM Branches { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
        {
			var endpoint = GetPageUrl(searchString, pageNumber, pageSize);
			var apiResponse = await GetApiResponseAsync<Page<DesignerDTO>>(endpoint);

            Designers = apiResponse.Value.Collection.Select(x => x.ToDesignerVM());
            PageInfo = GetPageInfo(apiResponse, searchString);

            return Page();
        }

        public async Task<IActionResult> OnGetLicensesPartialAsync(int id)
        {
            try
            {
                var apiResponse = await GetApiResponseAsync<DesignerDTO>($"designers/{id}/details");

                return GetPartialView(apiResponse.Value.ToDesignerVM(), _licensesPartialViewName);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        //      public async Task<IActionResult> OnGetEditDesignerAsync(int? id)
        //      {
        //	var model = new DesignerVM();
        //	if (id != null)
        //	{
        //		var apiResponse = await _apiProvider.GetAsync($"designers/{id}");
        //		if (!apiResponse.IsSuccessStatusCode)
        //		{
        //			RedirectToPage("Errors/404");
        //		}
        //		var response = await apiResponse.Content.ReadAsAsync<ApiResponse<DesignerVM>>();
        //              model.Id = response.Value.Id;
        //              model.Title = response.Value.Title;
        //              model.Firstname = response.Value.Firstname;
        //              model.Lastname = response.Value.Lastname;

        //          }

        //	return GetPartialView(model, _editDesignerModalViewName);
        //}

        //      public async Task<IActionResult> OnPostEditDesignerAsync(DesignerVM designer)
        //      {
        //          if (ModelState.IsValid)
        //          {
        //              if (designer.Id == 0)
        //              {
        //                  await _apiProvider.PostAsync($"{_endpoint}", designer.ToHttpContent());
        //              }
        //              else
        //              {
        //                  await _apiProvider.PutAsync($"{_endpoint}/{designer.Id}", designer.ToHttpContent());
        //              }
        //          }

        //          return GetPartialView(designer, _editDesignerModalViewName);
        //      }


        //      public IActionResult OnGetNewLicense(int id)
        //      {
        //	var model = new NewLicenseVM
        //	{
        //		DesignerId = id,
        //		Content = ""
        //	};

        //	return GetPartialView(model, _newLicenseModalViewName);
        //}

        //public async Task<IActionResult> OnPostNewLicenseAsync(NewLicenseVM license)
        //      {
        //          if (ModelState.IsValid)
        //          {
        //              var apiResponse = await _apiProvider.PostAsync($"licenses/", license.ToHttpContent());
        //              if (!apiResponse.IsSuccessStatusCode)
        //              {
        //                  var content = await _apiProvider.GetContentAsync<ApiResponse>(apiResponse);
        //                  SetModelErrors(content);
        //              }
        //          }

        //          return GetPartialView(license, _newLicenseModalViewName);
        //      }
    }
}
