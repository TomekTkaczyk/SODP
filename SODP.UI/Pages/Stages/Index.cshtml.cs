
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Designers.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Pages.Stages.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Stages
{
    [Authorize(Roles = "Administrator, ProjectManager")]
    public class IndexModel : ListPageModel
    {
        const string editStagePartialViewName = "_EditStagePartialView";
        const string deleteStagePartialViewName = "_DeleteStagePartialView";

        public StagesListVM StagesViewModel { get; set; }

        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger) : base(apiProvider, logger)
        {
            ReturnUrl = "/Stages";
            _endpoint = "stages";
        }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            var url = new StringBuilder();
            url.Append(ReturnUrl);
            url.Append("?currentPage=:&pageSize=");
            pageSize = pageSize < 1 ? PageSizeSelectList.PageSizeList[0] : pageSize;
            url.Append(pageSize.ToString());

            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
            {
                url.Append($"&searchString={searchString}");
            }

            StagesViewModel = new StagesListVM
            {
                Stages = await GetStagesAsync(currentPage, pageSize, searchString)
            };

            SearchString = searchString;
            PageInfo.ItemsPerPage = pageSize;
            PageInfo.Url = url.ToString();

            return Page();
        }

        public PartialViewResult OnGetDeleteStage(int? id)
        {
            return GetPartialView(id, deleteStagePartialViewName);
        }

        public async Task<PartialViewResult> OnPostDeleteStageAsync(int id)
        {
            if (id != 0)
            {
                var apiResponse = await _apiProvider.DeleteAsync($"stages/{id}");
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var response = await _apiProvider.GetContent<ServiceResponse<int>>(apiResponse);
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

            return GetPartialView(id, deleteStagePartialViewName);
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

                // Cos posz³o nie tak !!!
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

        private async Task<IList<StageDTO>> GetStagesAsync(int currentPage, int pageSize, string searchString)
        {
            var apiResponse = await _apiProvider.GetAsync($"{_endpoint}?currentPage={currentPage}&pageSize={pageSize}&searchString={searchString}");

            if (apiResponse.IsSuccessStatusCode)
            {
                var result = await apiResponse.Content.ReadAsAsync<ServicePageResponse<StageDTO>>();
                PageInfo.TotalItems = result.Data.TotalCount;
                PageInfo.CurrentPage = result.Data.PageNumber;

                return result.Data.Collection.ToList();
            }

            return new List<StageDTO>();
        }
    }
}