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

        public ServicePageResponse<StageDTO> Stages { get; set; }
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
            var stage = (id == null) ? new StageDTO() : (await _stagesService.GetAsync((int)id)).Data;

            var viewModel = new NewStageVM()
            {
                Stage = stage
            };

            var partialViewResult = new PartialViewResult
            {
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<NewStageVM>(ViewData, viewModel)
            };

            return await Task.FromResult(partialViewResult);
        }

        public async Task<PartialViewResult> OnPostNewStageAsync(StageDTO stage)
        {
            if (ModelState.IsValid)
            {
                var response = (stage.Id == 0) ? await _stagesService.CreateAsync(stage) : await _stagesService.UpdateAsync(stage);
                if (!response.Success)
                {
                    foreach(var error in response.ValidationErrors)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                }
            }

            var partialViewResult = new PartialViewResult
            {
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<StageDTO>(ViewData, stage)
            };

            return partialViewResult;
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
    }
}
