
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using SODP.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Stages
{
    [Authorize(Roles = "Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {

        const string partialViewName = "_NewStagePartialView";
        private readonly IWebAPIProvider _apiProvider;

        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/Stages";
            _apiProvider = apiProvider;
        }

        public StagesListVM StagesViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 15)
        {
            var url = new StringBuilder();
            url.Append("/Stages?currentPage=:&pageSize=");
            url.Append(pageSize.ToString());

            StagesViewModel = new StagesListVM
            {
                PageInfo = new PageInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = pageSize,
                    Url = url.ToString()
                },
            };
            StagesViewModel.Stages = await GetStagesAsync(StagesViewModel.PageInfo);

            return Page();
        }


        public async Task<PartialViewResult> OnGetNewStageAsync(int? id)
        {
            if (id != null)
            {
                var apiResponse = await _apiProvider.GetAsync($"/stages/{id}");
                if (apiResponse.IsSuccessStatusCode)
                {
                    var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();

                    return GetPartialView(result.Data);
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
                    var apiResponse = await _apiProvider.PostAsync($"/stages/{stage.Sign}", body);
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
                    var apiResponse = await _apiProvider.PutAsync($"/stages/{stage.Sign}", body);
                    var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();
                    if (response.Success)
                    {
                        apiResponse = await _apiProvider.GetAsync($"/stages/{stage.Id}");
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

        private async Task<IList<StageDTO>> GetStagesAsync(PageInfo pageInfo)
        {
            var apiResponse = await _apiProvider.GetAsync($"/stages?currentPage={pageInfo.CurrentPage}&pageSize={pageInfo.ItemsPerPage}");

            if (apiResponse.IsSuccessStatusCode)
            {
                var result = await apiResponse.Content.ReadAsAsync<ServicePageResponse<StageDTO>>();
                pageInfo.TotalItems = result.Data.TotalCount;
                pageInfo.CurrentPage = result.Data.PageNumber;

                return result.Data.Collection.ToList();
            }

            return new List<StageDTO>();
        }

        private PartialViewResult GetPartialView(StageDTO stage)
        {
            var viewModel = new NewStageVM()
            {
                Stage = stage
            };

            return new PartialViewResult()
            {
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<NewStageVM>(ViewData, viewModel)
            };
        }
    }
}