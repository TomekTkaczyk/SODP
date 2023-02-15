using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Branches.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Branches
{
    [Authorize(Roles = "ProjectManager")]
    public class IndexModel : ListPageModel<BranchDTO>
	{
        const string _editBranchPartialViewName = "_EditBranchPartialView";
        const string _designersPartialViewName = "_DesignersPartialView";
        
        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
        {
            ReturnUrl = "/Branches";
            _endpoint = "branches";
        }

        public BranchesVM Branches { get; set; }

        public DesignersVM Designers { get; set; }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            var endpoint = GetUrl(currentPage, pageSize, searchString);
			var apiResponse = await GetApiResponseAsync(endpoint);
			
            PageInfo = GetPageInfo(apiResponse, searchString);
            Branches = new BranchesVM
            {
                Branches = apiResponse.Data.Collection.ToList(),
                PageInfo = PageInfo
            };

            return Page();
        }

        public async Task<PartialViewResult> OnGetEditBranchAsync(int? id)
        {
            var model = new BranchVM();
            if (id != null)
            {
                var apiResponse = await _apiProvider.GetAsync($"{_endpoint}/{id}");
                if (apiResponse.IsSuccessStatusCode)
                {
                    var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<BranchDTO>>();
                    model = result.Data.ToViewModel();
                }
            }

            return GetPartialView(model, _editBranchPartialViewName);
        }

        public async Task<PartialViewResult> OnPostEditBranchAsync(BranchVM model)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = model.Id == 0 
                    ? await _apiProvider.PostAsync($"{_endpoint}", model.ToHttpContent())
                    : await _apiProvider.PutAsync($"{_endpoint}/{model.Id}", model.ToHttpContent());

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

            return GetPartialView(model, _editBranchPartialViewName);
        }

        public async Task<PartialViewResult> OnGetPartialDesignersAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"{_endpoint}/{id}/designers");
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

            return GetPartialView<DesignersVM>(Designers, _designersPartialViewName);
        } 
    }
}
