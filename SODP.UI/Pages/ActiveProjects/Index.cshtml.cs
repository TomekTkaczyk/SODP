using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects
{
//    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : ProjectsPageModel
    {
        const string newProjectPartialViewName = "_NewProjectPartialView";

        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper, ITranslator translator) : base(apiProvider, logger, mapper, translator)
        {
            ReturnUrl = "/ActiveProjects";
            _endpoint = "projects";
        }

        public async Task<IActionResult> OnGetAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            return await base.OnGetAsync(ProjectStatus.Active, currentPage, pageSize, searchString);
        }

        public async Task<IActionResult> OnGetNewProjectAsync()
        {
            return await GetPartialViewAsync(new NewProjectVM());
        }

        public async Task<PartialViewResult> OnPostNewProjectAsync(NewProjectVM project)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PostAsync(_endpoint, project.ToHttpContent());

                var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);

                if (apiResponse.IsSuccessStatusCode && response.Success)
                {
                    return await GetPartialViewAsync(new NewProjectVM
                    {
                        Id = response.Data.Id,
                        Number = response.Data.Number,
                        StageId = response.Data.Stage.Id
                    });
                }
                else
                {
                    SetModelErrors(response);
                }
            }

            return await GetPartialViewAsync(project);
        }

        private async Task<PartialViewResult> GetPartialViewAsync(NewProjectVM project)
        {
            var response = await _apiProvider.GetAsync("stages");
            List<SelectListItem> stagesItems = new List<SelectListItem>();
            if (response.IsSuccessStatusCode)
            {
                var stages = await response.Content.ReadAsAsync<ServicePageResponse<StageDTO>>();
                stagesItems = stages.Data.Collection
                    .Where(x => x.ActiveStatus)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.ToString()
                    }).ToList();

                if (project.StageId != 0)
                {
                    var currentStage = stages.Data.Collection.FirstOrDefault(x => x.Id == project.StageId);
                }
            }
            project.Stages = stagesItems;

            return new PartialViewResult
            {
                ViewName = newProjectPartialViewName,
                ViewData = new ViewDataDictionary<NewProjectVM>(ViewData, project)
            };
        }
    }
}
