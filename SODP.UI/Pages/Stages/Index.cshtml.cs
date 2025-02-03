
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Pages.Stages.ViewModels;
using SODP.UI.Services;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Stages
{
	[Authorize(Roles = "ProjectManager")]
    public class IndexModel : ListPageModel<StageDTO>
    {
        const string _editStageModalViewName = "ModalView/_EditStageModalView";

        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
        {
            ReturnUrl = "/Stages";
            _endpoint = "stages";
        }

        public StagesVM Stages { get; set; }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            var endpoint = GetUrl(currentPage, pageSize, searchString);
            var apiResponse = await GetApiResponseAsync(endpoint);
            
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
            var model = new StageVM();
            if (id == null)
            {
                return GetPartialView(model, _editStageModalViewName);
            }

            var apiResponse = await _apiProvider.GetAsync($"{_endpoint}/{id}");
            if (!apiResponse.IsSuccessStatusCode)
            {
				RedirectToPage($"Errors/{(int)apiResponse.StatusCode}");
			}

			var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();
            model = _mapper.Map<StageVM>(result.Data);

            return GetPartialView(model, _editStageModalViewName);
        }

        public async Task<PartialViewResult> OnPostEditStageAsync(StageVM stage)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = stage.Id == 0
                    ? await _apiProvider.PostAsync($"{_endpoint}", stage.ToHttpContent())
                    : await _apiProvider.PutAsync($"{_endpoint}/{stage.Id}", stage.ToHttpContent());
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

            return  GetPartialView(stage, _editStageModalViewName);
        }
    }
}
