using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using SODP.UI.Extensions;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using SODP.UI.Pages.Shared;
using SODP.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects
{
	[Authorize(Roles = "Administrator,ProjectManager")]
	public class EditModel : ProjectEditPageModel
	{
		const string _technicalRolesPartialViewName = "_TechnicalRolesPartialView";
		const string _investorsPartialViewName = "_InvestorsPartialView";
		const string _projectPartPartialViewName = "_EditProjectPartPartialView";
		const string _partBranchesPartialViewName = "_PartBranchesPartialView";

		public EditModel(IWebAPIProvider apiProvider, ILogger<EditModel> logger, IMapper mapper, ITranslator translator) : base(apiProvider, logger, mapper, translator) { }

		public ProjectVM Project { get; set; }

		public InvestorsVM Investors { get; set; }

		public TechnicalRoleVM TechnicalRoles { get; set; }

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Project = await GetProjectAsync(id);

			if (Project == null)
			{
				return Redirect("/Errors/404");
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(ProjectVM project)
		{
			if (ModelState.IsValid)
			{
				var apiResponse = await _apiProvider.PutAsync($"projects/{project.Id}", project.ToHttpContent());
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
			var apiResponse = await _apiProvider.PutAsync($"projects/{id}/branches/{branchId}", new StringContent(
								  JsonSerializer.Serialize(branchId),
								  Encoding.UTF8,
								  "application/json"
							  ));

			Project = await GetProjectAsync(id);

			return Page();
		}

		public async Task<IActionResult> OnDeleteBranchAsync(int id, int branchId)
		{
			await _apiProvider.DeleteAsync($"projects/{id}/branches/{branchId}");

			Project = await GetProjectAsync(id);

			return Page();
		}

		public async Task<IActionResult> OnGetTechnicalRolesAsync(int projectId, int branchId)
		{
			return await GetPartialBranchesViewAsync(projectId, branchId);
		}

		public async Task<IActionResult> OnPostTechnicalRolesAsync(TechnicalRoleVM role)
		{
			if (ModelState.IsValid)
			{
				var apiResponse = await _apiProvider
					.PatchAsync(
						$"projects/{role.ProjectId}/branch/{role.BranchId}/role/{role.RoleId}",	
						role.ToHttpContent()
					);

				var response = await _apiProvider.GetContent<ServiceResponse<object>>(apiResponse);

				if (apiResponse.IsSuccessStatusCode && response.Success)
				{
					return await GetPartialBranchesViewAsync(role.ProjectId, role.BranchId);
				}

				SetModelErrors(response);
			}

			return await GetPartialBranchesViewAsync(role.ProjectId, role.BranchId);
		}

		public async Task<PartialViewResult> OnGetInvestorsListAsync(int projectId)
		{
			var apiResponse = await _apiProvider.GetAsync($"investors");
			if (apiResponse.IsSuccessStatusCode)
			{
				var response = await _apiProvider.GetContent<ServicePageResponse<InvestorDTO>>(apiResponse);
				var model = new InvestorsVM()
				{
					ProjectId = projectId,
					Investors = response.Data.Collection
						.Where(x => x.ActiveStatus)
						.Select(x => new SelectListItem
						{
							Value = x.Id.ToString(),
							Text = x.Name.ToString()
						})
						.ToList()
				};

				return GetPartialView(model, _investorsPartialViewName);
			}

			// for example show error message
			return GetPartialView(new InvestorsVM(), _investorsPartialViewName);
		}

		public async Task<PartialViewResult> OnPostInvestorsList(InvestorsVM investors)
		{
			investors.Investors = new List<SelectListItem>();
			var apiResponse = await _apiProvider.GetAsync($"investors/{investors.InvestorId}");
			var response = await _apiProvider.GetContent<ServiceResponse<InvestorDTO>>(apiResponse);
			if (apiResponse.IsSuccessStatusCode)
			{
				if (response.Success)
				{
					apiResponse = await _apiProvider.PatchAsync($"projects/{investors.ProjectId}/investor", new StringContent(
								  JsonSerializer.Serialize(response.Data.Name),
								  Encoding.UTF8,
								  "application/json"
					));
					if(!apiResponse.IsSuccessStatusCode)
					{
						SetModelErrors(response);
					}
				}
			}

			return GetPartialView(investors, _investorsPartialViewName);
		}

		public async Task<PartialViewResult> OnGetEditProjectPartAsync(int projectId, int projectPartId)
        {
			var items = await GetPartsAsync();
			var model = new PartVM()
			{
				Id = projectPartId,
				ProjectId = projectId,
				Items = items.Select(x => new SelectListItem 
				{ 
					Value= x.Sign,
					Text = x.Name, 
				}).ToList()
			};
			var part = await GetProjectPart(projectPartId);
			if(part != null)
			{
				model.Sign = part.Sign;
				model.Name = part.Name;
			}

            return GetPartialView(model, _projectPartPartialViewName);
        }

		private async Task<ProjectPartDTO> GetProjectPart(int projectPartId)
		{
			var apiResponse = await _apiProvider.GetAsync($"projects/parts/{projectPartId}");
			if (apiResponse.IsSuccessStatusCode)
			{
				var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<ProjectPartDTO>>();
				return result.Data;
			}

			return null;
		}

		public async Task<PartialViewResult> OnPostEditProjectPartAsync(PartVM part)
        {
			part.Items = new List<SelectListItem>();
			if (ModelState.IsValid)
			{
				HttpResponseMessage apiResponse;
				if(part.Id== 0)
				{
					apiResponse = await _apiProvider.PostAsync($"projects/{part.ProjectId}/parts", part.ToHttpContent());
				}
				else
				{
					apiResponse = await _apiProvider.PutAsync($"projects/parts/{part.Id}", part.ToHttpContent());
				}
				switch (apiResponse.StatusCode)
				{
					case HttpStatusCode.OK:
						var response = await _apiProvider.GetContent<ServiceResponse>(apiResponse);
						if (!response.Success)
						{
							SetModelErrors(response);
						}
						break;
					default:
						// redirect to ErrorPage
						break;
				}
			}

			return GetPartialView(part, _projectPartPartialViewName);
        }

		public async Task<PartialViewResult> OnGetPartBranchesPartialAsync(int id)
		{
			var model = new PartBranchesVM();

			var apiResponse = await _apiProvider.GetAsync($"projects/parts/{id}/branches");
			if(apiResponse.IsSuccessStatusCode)
			{
				var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<PartBranchDTO>>();
				model.ProjectPart = _mapper.Map<PartDTO,ProjectPartVM>(result.Data.Part);
				model.Branches = _mapper.Map<ICollection<BranchRoleDTO>,IList<PartBranchVM>>(result.Data.Roles);
			}

			return GetPartialView(model, _partBranchesPartialViewName);
		}


		private async Task<PartialViewResult> GetPartialBranchesViewAsync(int projectId, int branchId)
		{
			var apiResponse = await _apiProvider.GetAsync($"projects/{projectId}/branches/{branchId}");
			var responseRoles = await _apiProvider.GetContent<ServicePageResponse<ProjectBranchRoleDTO>>(apiResponse);

			var technicalRoles = new TechnicalRoleVM
			{
				ProjectId = projectId,
				BranchId = branchId,

				AvailableRoles = Enum.GetValues(typeof(TechnicalRole))
				.Cast<TechnicalRole>()
				.Select(x => new SelectListItem
				{
					Value = ((int)x).ToString(),
					Text = _translator.Translate(x.ToString(), Languages.Pl)
				}).ToList(),

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

			apiResponse = await _apiProvider.GetAsync($"licenses/branch/{branchId}");
			var responseLicenses = await _apiProvider.GetContent<ServicePageResponse<LicenseDTO>>(apiResponse);

			technicalRoles.Licenses = responseLicenses.Data.Collection
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = $"{x.Designer} ({x.Content})"
				}).ToList();
			technicalRoles.Licenses.Add(new SelectListItem("-- usuñ z listy --", "0"));

			return GetPartialView(technicalRoles, _technicalRolesPartialViewName);
		}

		private async Task<IList<PartVM>> GetPartsAsync()
		{
            var apiResponse = await _apiProvider.GetAsync($"parts");
            if (apiResponse.IsSuccessStatusCode)
            {
				var result = await apiResponse.Content.ReadAsAsync<ServicePageResponse<PartDTO>>();
				return _mapper.Map<ICollection<PartVM>>(result.Data.Collection).ToList();
            }

			return new List<PartVM>();
        }
    }
}

