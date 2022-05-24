
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using SODP.UI.Pages.Stages.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Stages
{
    [Authorize(Roles = "Administrator, ProjectManager")]
    public class IndexModel : ListPageModel
    {
        const string newStagePartialViewName = "_NewStagePartialView";

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


        public async Task<PartialViewResult> OnGetNewStageAsync(int? id)
        {
            if (id != null)
            {
                var apiResponse = await _apiProvider.GetAsync($"stages/{id}");
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();

                    return GetPartialView(response.Data.ToViewModel(), newStagePartialViewName);
                }
            }

            return GetPartialView(new StageDTO());
        }

        public async Task<PartialViewResult> OnPostNewStageAsync(StageDTO stage)
        {
            if (ModelState.IsValid)
            {
                var body = new StringContent(JsonSerializer.Serialize(stage), Encoding.UTF8, "application/json");
                if(stage.Id == 0)
                {
                    var apiResponse = await _apiProvider.PostAsync($"stages/{stage.Sign}", body);
                    var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();
                    if (response.Success)
                    {
                        stage = response.Data;
                    }
                    else
                    {
                        SetModelErrors(response);
                    }
                }
                else
                {
                    var apiResponse = await _apiProvider.PutAsync($"stages/{stage.Sign}", body);
                    var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();
                    if (response.Success)
                    {
                        apiResponse = await _apiProvider.GetAsync($"stages/{stage.Id}");
                        response = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();
                        if (response.Success)
                        {
                            stage = response.Data;
                        }
                        else
                        {
                            SetModelErrors(response);
                        }
                    }
                    else
                    {
                        SetModelErrors(response);
                    }
                }
            }

            return GetPartialView(stage);
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

        private PartialViewResult GetPartialView(StageDTO stage)
        {
            var viewModel = new NewStageVM()
            {
                Sign = stage.Sign,
                Title = stage.Title
            };

            return new PartialViewResult()
            {
                ViewName = newStagePartialViewName,
                ViewData = new ViewDataDictionary<NewStageVM>(ViewData, viewModel)
            };
        }
    }
}