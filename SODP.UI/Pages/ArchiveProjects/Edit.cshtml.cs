using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using SODP.UI.ViewModels;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ArchiveProjects
{
    [Authorize(Roles = "Administrator,ProjectManager,User")]
    public class EditModel : SODPPageModel
    {
        private readonly IWebAPIProvider _apiProvider;

        public EditModel(IWebAPIProvider apiProvider, ILogger<EditModel> logger) : base(logger)
        {
            _apiProvider = apiProvider;
        }

        public ProjectVM Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"archive-projects/{id}");
            var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);

            if (apiResponse.IsSuccessStatusCode && response.Success)
            {
                Project = new ProjectVM
                {
                  Id = response.Data.Id,
                  Number = response.Data.Number,
                  StageId = response.Data.Stage.Id,
                  StageSign = response.Data.Stage.Sign,
                  StageTitle = response.Data.Stage.Title,
                  Title = response.Data.Title,
                  Description = response.Data.Description,
                  Status = response.Data.Status
                };
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
                Project = new ProjectVM
                {
                    Status = ProjectStatus.Archived
                };
            }

            return Page();
        }
    }
}
