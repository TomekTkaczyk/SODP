﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared.ViewModels;
using SODP.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.Shared.PageModels
{
    public abstract class ProjectEditPageModel : SODPPageModel
    {
        protected readonly IWebAPIProvider _apiProvider;

        public ProjectEditPageModel(IWebAPIProvider apiProvider, ILogger<SODPPageModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(logger, mapper, translatorFactory)
        {
            _apiProvider = apiProvider;
        }

        protected virtual async Task<ProjectVM> GetProjectAsync(int id)
        {
            var apiResponse = await _apiProvider.GetAsync($"projects/{id}/details");
            var response = await _apiProvider.GetContent<ServiceResponse<ProjectDTO>>(apiResponse);

            if (response.Success)
            {
                var parts = _mapper.Map<ICollection<ProjectPartVM>>(response.Data.Parts).ToList();
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
                    DevelopmentDate = response.Data.DevelopmentDate == null ? null : ((DateTime)response.Data.DevelopmentDate).ToString("yyyy-MM-dd"),
                    Status = response.Data.Status,
                    BuildingPermit = response.Data.BuildingPermit,
                    Parts = parts,
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

        //private async Task<IList<PartVM>> GetPartsAsync(IList<PartVM> exclusionList)
        //{
        //	var apiResponse = await _apiProvider.GetAsync($"parts?active=true");
        //	var response = await _apiProvider.GetContent<ServicePageResponse<ProjectPartDTO>>(apiResponse);

        //          return response.Data.Collection
        //              .Where(y => exclusionList.SingleOrDefault(z => z.Sign == y.Sign) == null)
        //              .OrderBy(x => x.Sign)
        //              .Select(x => new PartVM
        //              {
        //                  Id= x.Id,
        //                  Sign = x.Sign,
        //                  Name = x.Name,
        //              }).ToList();
        //}
    }
}
