
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
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Stages
{
    [Authorize(Roles = "ProjectManager")]
    public class IndexModel : ListPageModel
    {
        const string editStagePartialViewName = "_EditStagePartialView";


        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, ITranslator translator) : base(apiProvider, logger, mapper, translator)
        {
            ReturnUrl = "/Stages";
            _endpoint = "stages";
        }

        public StagesVM Stages { get; set; }


        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            var url = new StringBuilder();
            url.Append(ReturnUrl);
            url.Append("?currentPage=:&pageSize=");
            pageSize = pageSize < 1 ? PageSizeSelectList.PageSizeList[0] : pageSize;
            url.Append(pageSize);

            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
            {
                url.Append($"&searchString={searchString}");
            }

            Stages = new StagesVM
            {
				PageInfo = new PageInfo
				{
					CurrentPage = currentPage,
					ItemsPerPage = pageSize,
					Url = url.ToString()
				},
                SearchString = searchString
            };
            this.PageInfo = Stages.PageInfo;
            Stages.Stages = await GetStagesAsync(Stages.PageInfo, searchString);

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
                    ? await _apiProvider.PostAsync($"stages/{stage.Sign}", stage.ToHttpContent())
                    : await _apiProvider.PutAsync($"stages/{stage.Sign}", stage.ToHttpContent());
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

        private async Task<List<StageVM>> GetStagesAsync(PageInfo pageInfo, string searchString)
        {
            var apiResponse = await _apiProvider.GetAsync($"{_endpoint}?currentPage={pageInfo.CurrentPage}&pageSize={pageInfo.ItemsPerPage}&searchString={searchString}");

            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await _apiProvider.GetContent<ServicePageResponse<StageDTO>>(apiResponse);
                PageInfo.TotalItems = response.Data.TotalCount;
                PageInfo.CurrentPage = response.Data.PageNumber;

                return response.Data.Collection
                    .Select(x => new StageVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ActiveStatus = x.ActiveStatus
                    }).ToList();
            }

            return new List<StageVM>();
        }
    }
}