using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : ProjectsPageModel
    {
        const string partialViewName = "_NewProjectPartialView";

        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger) : base(apiProvider, logger)
        {
            ReturnUrl = "/ActiveProjects";
            _endpoint = "active-projects";
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

        //public async Task<IActionResult> OnPostDeleteAsync(int id)
        //{
        //    var response = await _apiProvider.DeleteAsync(_endpoint + $"/{id}");

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return Page();
        //    }

        //    return RedirectToPage("Index");
        //}

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
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<NewProjectVM>(ViewData, project)
            };
        }
    }
}
