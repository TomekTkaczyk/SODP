using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Mappers;
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
    [Authorize(Roles = "Administrator,ProjectManager")]
    public class EditModel : SODPPageModel
    {
        private readonly IWebAPIProvider _apiProvider;
        private readonly IMapper _mapper;

        public EditModel(IWebAPIProvider apiProvider, ILogger<EditModel> logger, IMapper mapper) : base(logger)
        {
            _apiProvider = apiProvider;
            _mapper = mapper;
        }

        public ProjectVM Project { get; set; }

        public IEnumerable<SelectListItem> Stages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await GetProjectAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPutBranchAsync(int id, int branchId)
        {
            await _apiProvider.PutAsync($"active-projects/{id}/branches/{branchId}", new StringContent(
                                  JsonSerializer.Serialize(branchId),
                                  Encoding.UTF8,
                                  "application/json"
                              ));

            await GetProjectAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnDeleteBranchAsync(int id, int branchId)
        {
            await _apiProvider.DeleteAsync($"active-projects/{id}/branches/{branchId}");

            await GetProjectAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(ProjectVM project)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _apiProvider.PutAsync($"active-projects/{project.Id}", project.ToHttpContent());
                if (apiResponse.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }

                var response = await _apiProvider.GetContent<ServiceResponse>(apiResponse);
                if (!string.IsNullOrEmpty(response.Message))
                {
                    ModelState.AddModelError("", response.Message);
                }
                foreach (var error in response.ValidationErrors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
          
            return Page();
        }

        private async Task GetProjectAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"active-projects/{id}/branches");
            var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);

            if (apiResponse.IsSuccessStatusCode)
            {
                Project = new ProjectVM
                {
                    Id = response.Data.Id,
                    Number = response.Data.Number,
                    StageId = response.Data.Stage.Id,
                    StageSign = response.Data.Stage.Sign,
                    StageTitle = response.Data.Stage.Title,
                    Title = response.Data.Title,
                    Status = response.Data.Status,
                    Address = response.Data.Address,
                    Investment = response.Data.Investment,
                    Investor = response.Data.Investor,
                    Description = response.Data.Description,
                    TitleStudy = response.Data.TitleStudy,
                    ApplyBranches = response.Data.Branches
                    .Select(x => new SelectListItem
                    {
                        Value = x.BranchId.ToString(),
                        Text = x.Name
                    })
                    .OrderBy(o => o.Text)
                    .ToList(),
                };
                Project.Branches = await GetBranchesAsync(Project.ApplyBranches);
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

        private async Task<List<SelectListItem>> GetBranchesAsync(List<SelectListItem> exclusionList)
        {
            var apiResponse = await _apiProvider.GetAsync($"branches");
            var responseBranch = await _apiProvider.GetContent<ServicePageResponse<BranchDTO>>(apiResponse);
            var result = responseBranch.Data.Collection
                .OrderBy(x => x.Symbol)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                }).ToList();

            if (exclusionList != null)
            {
                var shortList = result.Where(y => !exclusionList.Contains(y, new SelectListItemComparer())).ToList();
                result = shortList;
            }

            return result;
        }

    }
}
