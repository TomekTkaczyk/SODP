using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.UI.Api;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Branches.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Branches
{
	[Authorize(Roles = "ProjectManager")]
    public class IndexModel : CollectionPageModel
	{
        const string _editBranchPartialViewName = "ModalView/_EditBranchModalView";
        const string _designersPartialViewName = "PartialView/_DesignersPartialView";
        
        public IndexModel(
            IWebAPIProvider apiProvider, 
            ILogger<IndexModel> logger, 
            IMapper mapper, 
            LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
        {
            ReturnUrl = "/Branches";
            _endpoint = "branches";
        }

		public IReadOnlyCollection<BranchDTO> Branches { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
        {
            var endpoint = GetPageUrl(pageNumber, pageSize, searchString);
			var apiResponse = await GetApiResponseAsync<Page<BranchDTO>>(endpoint);

            Branches = GetCollection(apiResponse);
            PageInfo = GetPageInfo(apiResponse, searchString);

            return Page();
        }

        public async Task<IActionResult> OnGetEditBranchAsync(int? id)
        {
            var model = new BranchVM();
            if (id != null)
            {
                var apiResponse = await GetApiResponseAsync<BranchDTO>($"{_endpoint}/{id}"); 
                if (!apiResponse.IsSuccess)
                {
					return RedirectToPage($"/Errors/{apiResponse.HttpCode}");
                }
				model = _mapper.Map<BranchVM>(apiResponse.Value);
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
                        var response = await _apiProvider.GetContent<SODP.Shared.Response.ServiceResponse<BranchDTO>>(apiResponse);
                        if (!response.Success)
                        {
                            // SetModelErrors(response);
                        }
                        break;
                    default:
                        // redirect to ErrorPage
                        break;
                }
            }

            return GetPartialView(model, _editBranchPartialViewName);
        }

        public async Task<IActionResult> OnGetPartialDesignersAsync(int id)
        {
            var apiResponse = await GetApiResponseAsync<BranchDTO>($"{_endpoint}/{id}/licenses");
            if (!apiResponse.IsSuccess)
            {
                return RedirectToPage($"/Errors/{apiResponse.HttpCode}");
            }
			
            return GetPartialView(apiResponse.Value.Licenses, _designersPartialViewName);
        } 
    }
}
