using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects
{
    [Authorize(Roles = "Administrator,ProjectManager")]
    public class EditModel : SODPPageModel
    {
        private readonly IWebAPIProvider _apiProvider;

        const string setDesignerPartialViewName = "_SetRolePartialView";

        public EditModel(IWebAPIProvider apiProvider, ILogger<EditModel> logger, IMapper mapper) : base(logger, mapper)
        {
            _apiProvider = apiProvider;
        }

        public ProjectVM Project { get; set; }
        public BranchesVM ProjectBranches { get; set; }
        public AvailableBranchesVM AvailableBranches { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
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

        public async Task<IActionResult> OnGetRoleAsync(int projectId, int branchId, int selector)
        {

            var designer = new SetDesignerVM() { DesignerId = 0 };

            var apiResponse = await _apiProvider.GetAsync($"licenses/branch/{branchId}");
            var response = await _apiProvider.GetContent<ServicePageResponse<LicenseDTO>>(apiResponse);
            designer.Designers = response.Data.Collection.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.Designer} ({x.Content})"
            }).ToList();
            designer.ProjectId = projectId;
            designer.Selector = selector;

            return GetPartialView(designer, setDesignerPartialViewName);
        }

        public async Task<IActionResult> OnPostDesignerAsync(SetDesignerVM designer)
        {
            var response = await _apiProvider.PutAsync("", new StringContent(""));
            
            return GetPartialView(designer, setDesignerPartialViewName);
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
                    StageName = response.Data.Stage.Name,
                    Name = response.Data.Name,
                    Title = response.Data.Title,
                    Address = response.Data.Address,
                    LocationUnit = response.Data.LocationUnit,
                    BuildingCategory = response.Data.BuildingCategory,
                    Investor = response.Data.Investor,
                    Description = response.Data.Description,
                    Status = response.Data.Status,
                };
                
                ProjectBranches = new BranchesVM
                {
                    Branches = _mapper.Map<IList<ProjectBranchVM>>(response.Data.Branches)
                };

                AvailableBranches = new AvailableBranchesVM
                {
                    Items = await GetBranchesAsync(ProjectBranches.Branches),
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
            }
        }

        private async Task<List<SelectListItem>> GetBranchesAsync(IList<ProjectBranchVM> exclusionList)
        {
            var url = $"branches?activeOnly=true"; 
            var apiResponse = await _apiProvider.GetAsync(url);
            var responseBranch = await _apiProvider.GetContent<ServicePageResponse<BranchDTO>>(apiResponse);
            var result = responseBranch.Data.Collection
                .OrderBy(x => x.Order)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Name.Trim()}"
                }).ToList();

            if (exclusionList != null)
            {
                var shortList = result.Where(y => exclusionList.FirstOrDefault(z => z.Branch.Id.ToString() == y.Value) == null).ToList();
                result = shortList;
            }

            return result;
        }
    }
}
