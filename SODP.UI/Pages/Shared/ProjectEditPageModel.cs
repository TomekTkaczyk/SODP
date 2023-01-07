using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared
{
    public abstract class ProjectEditPageModel : SODPPageModel
    {
        protected readonly IWebAPIProvider _apiProvider;

        public ProjectEditPageModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, ITranslator translator) : base(logger, mapper, translator)
        {
            _apiProvider = apiProvider;
        }

        protected virtual async Task<ProjectVM> GetProjectAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"projects/{id}/branches");
            var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);

            if (response.Success)
            {
                var project = new ProjectVM
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
                    DevelopmentDate = response.Data.DevelopmentDate == null ? null : ((DateTime)response.Data.DevelopmentDate).Date.ToShortDateString(),
                    Status = response.Data.Status,
                };
                project.AvailableBranches = new AvailableBranchesVM
                {
                    Items = await GetBranchesAsync(project.ProjectBranches.Branches),
                };

                return project;
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

                return null;
            }
        }

        private async Task<List<SelectListItem>> GetBranchesAsync(IList<ProjectBranchVM> exclusionList)
        {
            var apiResponse = await _apiProvider.GetAsync($"branches?active=true");
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
