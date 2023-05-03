using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.UI.Api;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Parts.ViewModels;
using SODP.UI.Pages.Shared.PageModels;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Parts
{
	public class IndexModel : CollectionPageModel
	{
		const string _editPartModalViewName = "ModalView/_EditPartModalView";

		public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
        {
            ReturnUrl = "/Parts";
            _endpoint = "parts";
        }

        public IReadOnlyCollection<PartDTO> Parts { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 0, string searchString = "")
        {
			var endpoint = GetPageUrl(pageNumber, pageSize, searchString);
			var apiResponse = await GetApiResponseAsync<Page<PartDTO>>(endpoint);

			Parts = GetCollection(apiResponse);
			PageInfo = GetPageInfo(apiResponse, searchString);

			return Page();
        }

        public async Task<PartialViewResult> OnGetEditPartAsync(int? id)
        {
            var model = new PartVM();
			StringContent stringContent = model.ToHttpContent();
            if(id == null)
            {
                return GetPartialView(model, _editPartModalViewName);
            }

            var apiResponse = await _apiProvider.GetAsync($"{_endpoint}/{id}");
            if (apiResponse.IsSuccessStatusCode)
            {
				RedirectToPage($"Errors/{(int)apiResponse.StatusCode}");
			}

			var result = await apiResponse.Content.ReadAsAsync<SODP.Shared.Response.ServiceResponse<PartDTO>>();
            model = _mapper.Map<PartVM>(result.Data);

            return GetPartialView(model, _editPartModalViewName);
		}

		public async Task<PartialViewResult> OnPostEditPartAsync(PartVM model)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = model.Id == 0
                    ? await _apiProvider.PostAsync($"{_endpoint}", model.ToHttpContent())
                    : await _apiProvider.PutAsync($"{_endpoint}/{model.Id}", model.ToHttpContent());

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var response = await _apiProvider.GetContent<SODP.Shared.Response.ServiceResponse<PartDTO>>(apiResponse);
                        if (!response.Success)
                        {
                            // SetModelErrors(response);
                        }
                        break;
                    default:
                        break;
                }
            }

            return GetPartialView(model, _editPartModalViewName);
        }
    }
}
