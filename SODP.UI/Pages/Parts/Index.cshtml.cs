using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Parts.Extensions;
using SODP.UI.Pages.Parts.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Parts
{
    public class IndexModel : ListPageModel<PartDTO>
    {
		const string _editPartPartialViewName = "ModalView/_EditPartmodalView";

		public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory)
        {
            ReturnUrl = "/Parts";
            _endpoint = "parts";
        }

        public PartsVM Parts { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var endpoint = _endpoint;
            var apiResponse = await GetApiResponseAsync(endpoint);
            if (apiResponse != null)
            {
                Parts = new PartsVM
                {
                    Parts = apiResponse.Data.Collection.ToList(),
                };
            }

            return Page();
        }

        public async Task<PartialViewResult> OnGetEditPartAsync(int? id)
        {
            var model = new PartVM();
            if(id != null)
            {
                var apiResponse = await _apiProvider.GetAsync($"{_endpoint}/{id}");
                if (apiResponse.IsSuccessStatusCode)
                {
                    var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<PartDTO>>();
                    model = _mapper.Map<PartVM>(result.Data);
                }
            }

            return GetPartialView(model, _editPartPartialViewName);
        }

        public async Task<PartialViewResult> OnPostEditPartAsync(PartVM model)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = model.Id == 0
                    ? await _apiProvider.PostAsync($"", model.ToHttpContent())
                    : await _apiProvider.PostAsync($"", model.ToHttpContent())

			}

            return GetPartialView(model, _editPartPartialViewName);
        }
    }
}
