
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using SODP.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace SODP.UI.Pages.Stages
{
    [Authorize(Roles = "Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {
        private readonly IStageService _stagesService;

        const string partialViewName = "_NewStagePartialView";
        private readonly string _apiUrl;
        private readonly string _apiVersion;

        public IndexModel(IStageService stagesService, IWebAPIProvider apiProvider)
        {
            _stagesService = stagesService;

            ReturnUrl = "/Stages";
            _apiUrl = apiProvider.apiUrl;
            _apiVersion = apiProvider.apiVersion;
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
            StageDTO stage = new StageDTO();
            if (id != null)
            {
                var apiResponse = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/stages/{id}");
                if (apiResponse.IsSuccessStatusCode)
                {
                    var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();
                    stage = result.Data;
                }
            }

            return await Task.FromResult(GetPartialViewAsync(stage));
        }

        public async Task<PartialViewResult> OnPostNewStageAsync(StageDTO stage)
        {
            if (ModelState.IsValid)
            {
                var body = new StringContent(JsonSerializer.Serialize(stage), Encoding.UTF8, "application/json");
                if(stage.Id == 0)
                {
                    var apiResponse = await new HttpClient().PostAsync($"{_apiUrl}{_apiVersion}/stages/{stage.Sign}", body);
                    var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();
                    if (response.Success)
                    {
                        stage = response.Data;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(response.Message))
                        {
                            ModelState.AddModelError("", response.Message);
                        }
                        foreach (var error in response.ValidationErrors)
                        {
                            ModelState.AddModelError(error.Key, error.Value);
                        }
                    }
                }
                else
                {
                    var apiResponse = await new HttpClient().PutAsync($"{_apiUrl}{_apiVersion}/stages/{stage.Sign}", body);
                    var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();
                    if (response.Success)
                    {
                        apiResponse = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/stages/{stage.Id}");
                        response = await apiResponse.Content.ReadAsAsync<ServiceResponse<StageDTO>>();
                        if (response.Success)
                        {
                            stage = response.Data;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(response.Message))
                            {
                                ModelState.AddModelError("", response.Message);
                            }
                            foreach (var error in response.ValidationErrors)
                            {
                                ModelState.AddModelError(error.Key, error.Value);
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(response.Message))
                        {
                            ModelState.AddModelError("", response.Message);
                        }
                        foreach (var error in response.ValidationErrors)
                        {
                            ModelState.AddModelError(error.Key, error.Value);
                        }
                    }
                }
            }

            return GetPartialViewAsync(stage);
        }

        private async Task<IList<StageDTO>> GetStagesAsync(PageInfo pageInfo)
        {
            var stages = new List<StageDTO>();

            var response = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/stages?currentPage={pageInfo.CurrentPage}&pageSize={pageInfo.ItemsPerPage}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<ServicePageResponse<StageDTO>>();
                pageInfo.TotalItems = result.Data.TotalCount;
                pageInfo.CurrentPage = result.Data.PageNumber;

                stages = result.Data.Collection.ToList();
            }

            return stages;
        }

        private PartialViewResult GetPartialViewAsync(StageDTO stage)
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