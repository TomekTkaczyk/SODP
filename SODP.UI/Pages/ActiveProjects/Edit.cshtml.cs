using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using SODP.Model;
using SODP.Model.Enums;
using SODP.Shared.DTO;
using SODP.Shared.DTO.Requests;
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
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects
{
	[Authorize(Roles = "Administrator,ProjectManager")]
	public class EditModel : ProjectEditPageModel
	{
		const string _editProjectPartViewName = "ModalView/_EditProjectPartModalView";
		const string _addTechnicalRoleViewName = "ModalView/_AddTechnicalRoleModalView";
		const string _addPartBranchViewName = "ModalView/_AddPartBranchModalView";

		const string _investorsViewName = "_InvestorsPartialView";
		const string _partBranchesViewName = "_PartBranchesPartialView";
		const string _technicalRolesViewName = "_TechnicalRolesPartialView";

		public EditModel(IWebAPIProvider apiProvider, ILogger<EditModel> logger, IMapper mapper, LanguageTranslatorFactory translatorFactory) : base(apiProvider, logger, mapper, translatorFactory) { }

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

				return GetPartialView(model, _investorsViewName);
			}

			// for example show error message
			return GetPartialView(new InvestorsVM(), _investorsViewName);
		}

		public async Task<PartialViewResult> OnPostInvestorsListAsync(InvestorsVM investors)
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
					if (!apiResponse.IsSuccessStatusCode)
					{
						SetModelErrors(response);
					}
				}
			}

			return GetPartialView(investors, _investorsViewName);
		}

		public async Task<PartialViewResult> OnGetEditProjectPartAsync(int projectId, int projectPartId)
		{
			var model = new PartVM()
			{
				Id = projectPartId,
				ProjectId = projectId,
				Items = await GetAvailablePartsAsync(),
			};
			var part = await GetProjectPartAsync(projectPartId);
			if (part != null)
			{
				model.Sign = part.Sign;
				model.Name = part.Name;
			}

			return GetPartialView(model, _editProjectPartViewName);
		}

		public async Task<PartialViewResult> OnPostEditProjectPartAsync(PartVM part)
		{
			part.Items = await GetAvailablePartsAsync();
			if (ModelState.IsValid)
			{
				HttpResponseMessage apiResponse;
				if (part.Id == 0)
				{
					apiResponse = await _apiProvider.PostAsync($"projects/{part.ProjectId}/parts", part.ToHttpContent());
				}
				else
				{
					apiResponse = await _apiProvider.PutAsync($"projects/parts/{part.Id}", part.ToHttpContent());
				}
				if (apiResponse.StatusCode == HttpStatusCode.OK)
				{
					var response = await _apiProvider.GetContent<ServiceResponse>(apiResponse);
					if (!response.Success)
					{
						SetModelErrors(response);
					}
				}
			}

			return GetPartialView(part, _editProjectPartViewName);
		}

		public async Task<PartialViewResult> OnGetPartBranchesPartialAsync(int projectPartId)
		{
			var model = new ProjectPartVM();

			var apiResponse = await _apiProvider.GetAsync($"projects/parts/{projectPartId}/branches");
			if (apiResponse.IsSuccessStatusCode)
			{
				var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<ProjectPartDTO>>();
				model = _mapper.Map<ProjectPartVM>(result.Data);
			}

			return GetPartialView(model, _partBranchesViewName);
		}

		public async Task<PartialViewResult> OnGetAddPartBranchAsync(int projectPartId)
		{
			var model = new AvailableBranchesVM
			{
				ProjectPartId = projectPartId
			};

			var apiResponse = await _apiProvider.GetAsync($"branches?active=true");
			if (apiResponse.IsSuccessStatusCode)
			{
				var result = await apiResponse.Content.ReadAsAsync<ServicePageResponse<BranchDTO>>();
				model.Items = result.Data.Collection.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.ToString()
				}).ToList();

				apiResponse = await _apiProvider.GetAsync($"projects/parts/{projectPartId}");
				if (apiResponse.IsSuccessStatusCode)
				{
					var part = await apiResponse.Content.ReadAsAsync<ServiceResponse<ProjectPartDTO>>();
					foreach (var item in part.Data.Branches)
					{
						var selected = model.Items.FirstOrDefault(x => x.Value == item.Branch.Id.ToString());
						model.Items.Remove(selected);
					}
				}
			}

			return GetPartialView(model, _addPartBranchViewName);
		}

		public async Task<PartialViewResult> OnPostAddPartBranch(AvailableBranchesVM model)
		{
			model.Items = new List<SelectListItem>();
			if (ModelState.IsValid)
			{
				if (model.SelectedId.HasValue && (model.SelectedId != 0))
				{
					var apiResponse = await _apiProvider.PostAsync($"projects/parts/{model.ProjectPartId}/branches/{model.SelectedId}", new StringContent(""));
					if (apiResponse.StatusCode == HttpStatusCode.OK)
					{
						var response = await _apiProvider.GetContent<ServiceResponse>(apiResponse);
						if (!response.Success)
						{
							SetModelErrors(response);
						}
					}
				}
			}

			return GetPartialView(model, _addPartBranchViewName);
		}

		public async Task<PartialViewResult> OnGetAddTechnicalRoleAsync(int partBranchId)
		{
			var response = await PartBranchServiceResponseAsync(partBranchId);
			var roles = GetAvailableRoles(response.Data);
			var designers = await GetAvailableDesignersAsync(response.Data);

			var model = new AvailableRolesVM
			{
				PartBranchId = partBranchId,
				ItemsRole = roles,
				ItemsLicense = designers,
			};

			return GetPartialView(model, _addTechnicalRoleViewName);
		}

		public async Task<PartialViewResult> OnPostAddTechnicalRole(AvailableRolesVM model)
		{
			var partBranchResponse = await PartBranchServiceResponseAsync(model.PartBranchId);
			model.ItemsLicense = GetAvailableRoles(partBranchResponse.Data);
			model.ItemsRole = await GetAvailableDesignersAsync(partBranchResponse.Data);
			if (ModelState.IsValid)
			{
				var role = new NewPartBranchRoleDTO
				{
					partBranchId = model.PartBranchId,
					Role = (TechnicalRole)Enum.Parse(typeof(TechnicalRole), model.SelectedRoleId.ToString()),
					LicenseId = (int)model.SelectedLicenseId
				};
				var apiResponse = await _apiProvider.PostAsync($"projects/parts/branches/roles", new StringContent(JsonSerializer.Serialize(role), Encoding.UTF8, "application/json"));
				if (apiResponse.StatusCode == HttpStatusCode.OK)
				{
					var response = await _apiProvider.GetContent<ServiceResponse>(apiResponse);
					if (!response.Success)
					{
						SetModelErrors(response);
					}
				}
			}

			return GetPartialView(model, _addTechnicalRoleViewName);
		}


		private SelectList GetAvailableRoles(PartBranchDTO partBranch)
		{
			var roles = Enum.GetValues(typeof(TechnicalRole)).Cast<TechnicalRole>().Select(x => new SelectListItem
			{
				Value = ((int)x).ToString(),
				Text = x.ToString()
			}).ToList();

			foreach (var item in partBranch.Roles)
			{
				var existRole = roles.FirstOrDefault(x => x.Text.Equals(item.Role));
				if (existRole != null)
				{
					roles.Remove(existRole);
				}
			}

			return new SelectList(roles, "Value", "Text");
		}

		private async Task<SelectList> GetAvailableDesignersAsync(PartBranchDTO partBranch)
		{
			var apiResponse = await _apiProvider.GetAsync($"licenses/branches/{partBranch.Branch.Id}");
			var result = await apiResponse.Content.ReadAsAsync<ServicePageResponse<LicenseDTO>>();
			var licenses = result.Data.Collection.ToList();
			licenses.RemoveAll(x => partBranch.Roles.Select(x => x.License.Designer).Any(y => y.Id == x.Designer.Id));
			var designers = licenses.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = $"{x.Designer} ({x.Content})"					 
			}).ToList();

			return new SelectList(designers, "Value", "Text");
		}

		private async Task<ProjectPartDTO> GetProjectPartAsync(int projectPartId)
		{
			var apiResponse = await _apiProvider.GetAsync($"projects/parts/{projectPartId}");
			if (apiResponse.IsSuccessStatusCode)
			{
				var result = await apiResponse.Content.ReadAsAsync<ServiceResponse<ProjectPartDTO>>();
				return result.Data;
			}

			return null;
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

			return GetPartialView(technicalRoles, _technicalRolesViewName);
		}

		private async Task<IList<SelectListItem>> GetAvailablePartsAsync()
		{
			var apiResponse = await _apiProvider.GetAsync($"parts");
			if (apiResponse.IsSuccessStatusCode)
			{
				var result = await apiResponse.Content.ReadAsAsync<ServicePageResponse<PartDTO>>();
				return result.Data.Collection.Select(x => new SelectListItem
				{
					Value = x.Sign,
					Text = x.Name,
				}).ToList();
			}

			return new List<SelectListItem>();
		}

		private async Task<ServiceResponse<PartBranchDTO>> PartBranchServiceResponseAsync(int partBranchId)
		{
			ServiceResponse<PartBranchDTO> response = new();
            var apiResponse = await _apiProvider.GetAsync($"projects/parts/branches/{partBranchId}");
            if (apiResponse.IsSuccessStatusCode)
            {
                response = await apiResponse.Content.ReadAsAsync<ServiceResponse<PartBranchDTO>>();
				if (!response.Success)
				{
					response.SetError(response.Message,500);
				}
            }
			else
			{
				response.SetError("REST API error", 500);
			}

			return response;
        }
	}
}

