using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using SODP.UI.ViewModels;
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
        private readonly IProjectsService _projectsService;
        private readonly IStagesService _stagesService;
        const string partialViewName = "_NewProjectPartialView";
        private readonly string _apiUrl;

        public IndexModel(IProjectsService projectsService, IStagesService stagesService, IWebAPIProvider apiProvider)
        {
            _projectsService = projectsService;
            _stagesService = stagesService;
            ReturnUrl = "/ActiveProjects";
            _apiUrl = apiProvider.apiUrl;
        }

        public ServicePageResponse<ProjectDTO> Projects { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await new HttpClient().GetAsync(_apiUrl + "/v0_01/active-projects");
            if (response.IsSuccessStatusCode)
            {
                Projects = await response.Content.ReadAsAsync<ServicePageResponse<ProjectDTO>>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _projectsService.DeleteAsync(id);
            if (!response.Success)
            {
                Projects = await _projectsService.GetAllAsync();
                return Page();
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnGetNewProjectAsync()
        {
            return await GetPartialViewAsync(new ProjectDTO());
        }

        public async Task<PartialViewResult> OnPostNewProjectAsync(ProjectDTO project)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await new HttpClient()
                    .PostAsync(_apiUrl + "/v0_01/active-projects", 
                                new StringContent(           
                                    JsonSerializer.Serialize(project), 
                                    Encoding.UTF8, 
                                    "application/json"
                                ));

                var response = await apiResponse.Content.ReadAsAsync<ServiceResponse<ProjectDTO>>();

                if (apiResponse.IsSuccessStatusCode)
                {
                    return await GetPartialViewAsync(response.Data);
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
            var result = await GetPartialViewAsync(project);

            return result;
        }

        private async Task<PartialViewResult> GetPartialViewAsync(ProjectDTO project)
        {
            var stages = (await _stagesService.GetAllAsync(1, 0)).Data.Collection;
            var stagesItems = stages.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = $"({x.Sign.Trim()}) {x.Title.Trim()}"
            }).ToList();

            if(project.StageId != 0)
            {
                var currentStage = stages.FirstOrDefault(x => x.Id == project.StageId);
                project.StageSign = currentStage.Sign;
                project.StageTitle = currentStage.Title;
            }

            var viewModel = new NewProjectVM()
            { 
                Project = project,
                Stages = stagesItems
            };

            return new PartialViewResult
            {
                ViewName = partialViewName,
                ViewData = new ViewDataDictionary<NewProjectVM>(ViewData, viewModel)
            };
        }
    }
}
