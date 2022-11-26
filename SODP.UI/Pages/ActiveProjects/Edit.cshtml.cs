using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SODP.Model;
using SODP.Model.Enums;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
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
using System.Runtime.CompilerServices;
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

        const string technicalRolesPartialViewName = "_TechnicalRolesPartialView";

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

        public async Task<IActionResult> OnGetTechnicalRolesAsync(int projectId, int branchId)
        {

            var apiResponse = await _apiProvider.GetAsync($"active-projects/{projectId}/branches/{branchId}");
            var responseRoles = await _apiProvider.GetContent<ServicePageResponse<ProjectBranchRoleDTO>>(apiResponse);

            var technicalRoles = new TechnicalRolesVM
            {
                ProjectId = projectId,
                BranchId = branchId,
                Roles = responseRoles.Data.Collection
                .Where(y => Enum.TryParse(y.Role, out TechnicalRole role))
                .Select(x =>
                {
                    Enum.TryParse(x.Role, out TechnicalRole role);
                    return new
                    {
                        key = role,
                        license = new LicenseVM()
                        {
                            Content = x.License.Content,
                            Designer = x.License.Designer.ToString()
                        }
                    };
                }).ToDictionary(t => t.key, t => t.license)
            };

            technicalRoles.AvailableRoles = Enum.GetValues(typeof(TechnicalRole))
                .Cast<TechnicalRole>()
                .Select(x => new SelectListItem
                {
                    Value = ((int)x).ToString(),
                    Text = x.ToString()
                }).ToList();


            apiResponse = await _apiProvider.GetAsync($"licenses/branch/{branchId}");
            var responseLicenses = await _apiProvider.GetContent<ServicePageResponse<LicenseDTO>>(apiResponse);

            technicalRoles.Licenses = responseLicenses.Data.Collection
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Designer}"
                }).ToList();

            return GetPartialView(technicalRoles, technicalRolesPartialViewName);
        }

        //public async Task<PartialViewResult> OnPostTechnicalRolesAsync(TechnicalRolesVM designer)
        //{
        //    var response = await _apiProvider.PutAsync("", new StringContent(""));
            
        //    return GetPartialView(designer, technicalRolesPartialViewName);
        //}

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
            var apiResponse = await _apiProvider.GetAsync($"branches?activeOnly=true");
            var responseBranch = await _apiProvider.GetContent<ServicePageResponse<BranchDTO>>(apiResponse);
            
            return responseBranch.Data.Collection
                .Where(y => exclusionList.FirstOrDefault(z => z.Branch.Id.ToString() == y.Id.ToString()) == null)
                .OrderBy(x => x.Order)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Name.Trim()}"
                }).ToList();
        }
    }
}
