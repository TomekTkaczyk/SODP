
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using SODP.UI.Pages.Stages.ViewModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Stages
{
    [Authorize(Roles = "ProjectManager")]
    public class IndexModel : ListPageModel<StageDTO>
    {
        const string editStagePartialViewName = "_EditStagePartialView";

        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
        {
            ReturnUrl = "/Stages";
            _endpoint = "stages";
        }

        public StagesVM Stages { get; set; }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            var endpoint = GetUrl(currentPage, pageSize, searchString);
            var apiResponse = await GetApiResponse(endpoint);
            
            PageInfo = GetPageInfo(apiResponse, searchString);
			Stages = new StagesVM
			{
				Stages = apiResponse.Data.Collection.ToList(),
                PageInfo = PageInfo
			};

			return Page();
		}

        public async Task<PartialViewResult> OnGetEditStageAsync(int? id)
        {
            if (id != null)
            {
                var apiResponse = await _apiProvider.GetAsync($"stages/{id}");
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();

                    return GetPartialView(response.Data.ToViewModel(), editStagePartialViewName);
                }

                RedirectToPage("Errors/404");
            }

            return GetPartialView(new StageVM(), editStagePartialViewName);
        }

        public async Task<PartialViewResult> OnPostEditStageAsync(StageVM stage)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = stage.Id == 0
                    ? await _apiProvider.PostAsync($"stages", stage.ToHttpContent())
                    : await _apiProvider.PutAsync($"stages/{stage.Id}", stage.ToHttpContent());
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var response = await _apiProvider.GetContent<ServiceResponse<StageDTO>>(apiResponse);
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

            return  GetPartialView(stage, editStagePartialViewName);
        }
    }
}
