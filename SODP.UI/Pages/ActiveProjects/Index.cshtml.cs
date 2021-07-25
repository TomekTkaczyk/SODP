using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

namespace SODP.UI.Pages.ActiveProjects
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : SODPPageModel
    {
        const string partialViewName = "_NewProjectPartialView";
        private readonly string _apiUrl;
        private readonly string _apiVersion;

        public IndexModel(IWebAPIProvider apiProvider)
        {
            ReturnUrl = "/ActiveProjects";
            _apiUrl = apiProvider.apiUrl;
            _apiVersion = apiProvider.apiVersion;
        }

        public ServicePageResponse<ProjectDTO> Projects { get; set; }

        //public ProjectsListVM Projects { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/active-projects");
            if (response.IsSuccessStatusCode)
            {
                Projects = await response.Content.ReadAsAsync<ServicePageResponse<ProjectDTO>>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await new HttpClient().DeleteAsync($"{_apiUrl}{_apiVersion}/active-projects/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Page();
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnGetNewProjectAsync()
        {
            return await GetPartialViewAsync(new NewProjectVM());
        }

        public async Task<PartialViewResult> OnPostNewProjectAsync(NewProjectVM project)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await new HttpClient()
                    .PostAsync($"{_apiUrl}{_apiVersion}/active-projects", 
                                new StringContent(           
                                    JsonSerializer.Serialize(project), 
                                    Encoding.UTF8, 
                                    "application/json"
                                ));

                var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<ProjectDTO>>();

                if (apiResponse.IsSuccessStatusCode && response.Success)
                {
                    //return await GetPartialViewAsync(response.Data);
                    return await GetPartialViewAsync(new NewProjectVM
                    {
                        Id = response.Data.Id,
                        Number = response.Data.Number,
                        Title = response.Data.Title,
                        Description = response.Data.Description,
                        StageId = response.Data.Stage.Id
                    });
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

            return await GetPartialViewAsync(project);
        }

        private async Task<PartialViewResult> GetPartialViewAsync(NewProjectVM project)
        {
            var response = await new HttpClient().GetAsync($"{_apiUrl}{_apiVersion}/stages");
            List<SelectListItem> stagesItems = new List<SelectListItem>();
            if (response.IsSuccessStatusCode)
            {
                var stages = await response.Content.ReadAsAsync<ServicePageResponse<StageDTO>>();
                stagesItems = stages.Data.Collection.Select(x => new SelectListItem()
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
