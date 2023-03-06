using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Helpers;
using SODP.Domain.Managers;
using SODP.Infrastructure.Services;
using SODP.Model;
using SODP.Model.Enums;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public class ProjectService : AppService<Project, ProjectDTO>, IProjectService
    {
        private readonly IFolderManager _folderManager;

        public ProjectService(IMapper mapper, IFolderManager folderManager, IValidator<Project> validator, SODPDBContext context) : base(mapper, validator, context)
        {
            _folderManager = folderManager;
        }


        public async Task<ServiceResponse<ProjectDTO>> CreateAsync(NewProjectDTO newProject)
        {
            var serviceResponse = new ServiceResponse<ProjectDTO>();
            try
            {
                var exist = await _context.Projects.Include(x => x.Stage).SingleOrDefaultAsync(x => x.Number == newProject.Number && x.Stage.Id == newProject.StageId);
                if (exist != null)
                {
                    serviceResponse.SetData(_mapper.Map<ProjectDTO>(exist));
                    serviceResponse.SetError($"Błąd: Projekt {exist.Symbol} już istnieje.", 409);
                    return serviceResponse;
                }

                var project = _mapper.Map<Project>(newProject);
                var validationResult = await _validator.ValidateAsync(project);
                if (!validationResult.IsValid)
                {
                    serviceResponse.ValidationErrorProcess(validationResult);
                    return serviceResponse;
                }

                project.Normalize();
                project.Stage = await _context.Stages.SingleOrDefaultAsync(x => x.Id == project.StageId);
                project.Title = "";
                project.Address = "";
                project.LocationUnit = "";
                project.BuildingCategory = "";
                project.Investor = "";
                project.Description = "";
                var (Success, Message) = await _folderManager.CreateFolderAsync(project);
                if (!Success)
                {
                    throw new ApplicationException($"Error: {Message}");
                }

                var entity = _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                serviceResponse.SetData(_mapper.Map<ProjectDTO>(entity.Entity));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


        public async Task<ServicePageResponse<ProjectDTO>> GetPageAsync(ProjectStatus status = ProjectStatus.Active, int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            return await GetPageAsync(_query
                .Include(x => x.Stage)
                .Where(x => x.Status == status)
                .Where(x => string.IsNullOrWhiteSpace(searchString) || x.Name.Contains(searchString) || x.Number.Contains(searchString) || x.Title.Contains(searchString) || x.Description.Contains(searchString))
                .OrderBy(x => x.Number)
                .ThenBy(x => x.Stage.Sign), currentPage, pageSize);
        }


        public override async Task<ServiceResponse<ProjectDTO>> GetAsync(int id)
        {
            _query = _query.Include(s => s.Stage);

            return await base.GetAsync(id);
        }


        public async Task<ServiceResponse<ProjectDTO>> GetWithDetailsAsync(int id)
        {
            var serviceResponse = new ServiceResponse<ProjectDTO>();
            try
            {
                var project = await _context.Projects
                    .Include(s => s.Stage)
                    .Include(s => s.Parts).ThenInclude(s => s.Branches).ThenInclude(s => s.Roles).ThenInclude(s => s.License).ThenInclude(s => s.Designer)
                    .Include(s => s.Parts).ThenInclude(s => s.Branches).ThenInclude(s => s.Branch)
                    .SingleOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    serviceResponse.SetError($"Error: Project Id:{id} not found.", 404);
                }
                serviceResponse.SetData(_mapper.Map<ProjectDTO>(project));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse> UpdateAsync(ProjectDTO updateProject)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var oldProject = await _context.Projects.Include(x => x.Stage).SingleOrDefaultAsync(x => x.Id == updateProject.Id);
                if(oldProject == null)
                {
                    serviceResponse.SetError($"Błąd: Project Id:{updateProject.Id} nie odnaleziony.", 404);
                    return serviceResponse;
                }
                var project = _mapper.Map<Project>(updateProject);
                project.Stage = await _context.Stages.SingleOrDefaultAsync(x => x.Id == project.StageId);
                var validationResult = await _validator.ValidateAsync(project);
                if(!validationResult.IsValid)
                {
                    serviceResponse.ValidationErrorProcess(validationResult);

                    return serviceResponse;
                }

                project.Normalize();

                var (Success, Message) = await _folderManager.RenameFolderAsync(project, ProjectsFolder.Active);
                if (!Success)
                {
                    throw new ApplicationException($"Błąd: {Message}");
                }

                oldProject.Name = project.Name;
                oldProject.Title = project.Title;
                oldProject.Address = project.Address;
                oldProject.LocationUnit = project.LocationUnit;
                oldProject.BuildingCategory = project.BuildingCategory;
                oldProject.Investor = project.Investor;
                oldProject.BuildingPermit = project.BuildingPermit;
                oldProject.Description = project.Description;
                oldProject.ModifyTimeStamp = DateTime.UtcNow;
                oldProject.DevelopmentDate = project.DevelopmentDate;
                _context.Projects.Update(oldProject);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse> ArchiveAsync(int id)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var project = await _context.Projects.Include(x => x.Stage).SingleOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    serviceResponse.SetError($"Błąd: Project Id:{id} nie odnaleziony.", 401);
                    return serviceResponse;
                }

                project.Status = ProjectStatus.DuringArchive;
                await _context.SaveChangesAsync();

                var (Success, Message) = await _folderManager.ArchiveFolderAsync(project);
                if (!Success)
                {
                    project.Status = ProjectStatus.Active;
                    await _context.SaveChangesAsync();
                    throw new ApplicationException($"Błąd: {Message}");
                }
                
                project.Status = ProjectStatus.Archival;
                project.ModifyTimeStamp = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


        public override async Task<ServiceResponse> DeleteAsync(int id)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var project = await _context.Projects.Include(x => x.Stage).SingleOrDefaultAsync(x => x.Id == id); 
                if(project == null)
                {
                    serviceResponse.SetError($"Błąd: Projekt Id:{id} nie odnaleziony.", 401);
                    return serviceResponse;
                }
                var (Success, Message) = await _folderManager.DeleteFolderAsync(project);
                if (!Success)
                {
                    throw new ApplicationException($"Błąd: {Message}");
                }
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse> RestoreAsync(int id)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var project = await _context.Projects.Include(x => x.Stage).SingleOrDefaultAsync(x => x.Id == id); 
                if(project == null)
                {
                    serviceResponse.SetError($"Błąd: Project Id:{id} nie odnaleziony.", 401);
                    return serviceResponse;
                }
                project.Status = ProjectStatus.DuringRestore;
                await _context.SaveChangesAsync();
                
                var (Success, Message) = await _folderManager.RestoreFolderAsync(project);
                if (!Success)
                {
                    throw new ApplicationException($"Błąd: {Message}");
                }
                project.Status = ProjectStatus.Active;
                _context.Projects.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

		public async Task<ServiceResponse<ProjectPartDTO>> GetProjectPartAsync(int partId)
		{
			var serviceResponse = new ServiceResponse<ProjectPartDTO>();
			var part = await _context.ProjectParts
                .Include(x => x.Branches)
                .ThenInclude(x => x.Branch)
                .SingleOrDefaultAsync(x => x.Id == partId);
			if (part == null)
			{
				serviceResponse.SetError($"Error: ProjectPart {partId} not found.", 404);
			}

			serviceResponse.SetData(_mapper.Map<ProjectPart, ProjectPartDTO>(part));

			return serviceResponse;
		}


        public ServiceResponse DeleteBranchAsync(int id, int branchId)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                //var projectBranch = await _context.ProjectBranches.FirstAsync(x => x.ProjectId == id && x.BranchId == branchId);
                //_context.ProjectBranches.Remove(projectBranch);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


        public ServiceResponse SetBranchTechnicalRoleAsync(TechnicalRoleDTO technicalRole)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                //var partBranch = await _context.PartBranch
                //    .Include(x => x.Branches)
                //    .SingleOrDefaultAsync(x => x.PartBranchId == technicalRole.ProjectId && x.BranchId == technicalRole.BranchId);
                //if (projectBranch == null)
                //{
                //    serviceResponse.SetError("Branża projektu nie odnaleziona", 400);
                //    return serviceResponse;
                //}

                //var branchRole = projectBranch.Roles.SingleOrDefault(x => x.Role == technicalRole.Role);

                //if (technicalRole.LicenseId == 0)
                //{
                //    if(branchRole != null)
                //    {
                //        projectBranch.Roles.Remove(branchRole);
                //    }
                //}
                //else
                //{
                //    var license = await _context.Licenses
                //        .Include(x => x.Branches)
                //        .SingleOrDefaultAsync(x => x.Id == technicalRole.LicenseId);

                //    if (license == null)
                //    {
                //        serviceResponse.SetError("Uprawnienia nie odnalezione", 400);
                //        return serviceResponse;
                //    }

                //    if (license.Branches == null || license.Branches.SingleOrDefault(x => x.BranchId == technicalRole.BranchId) == null)
                //    {
                //        serviceResponse.SetError($"Uprawnienia nie obejmują branży", 400);
                //        return serviceResponse;
                //    }

                //    if(branchRole == null)
                //    {
                //        projectBranch.Roles.Add(new ProjectBranchRole() { License = license, Role = technicalRole.Role });
                //    } 
                //    else
                //    {
                //        branchRole.License = license;
                //    }
                //}
                //_context.ProjectBranches.Update(projectBranch);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }


		public async Task<ServiceResponse> SetInvestorAsync(int id, string investor)
		{
            var serviceResponse = new ServiceResponse();
            try
            {
                var project = await _context.Projects.FirstAsync(x => x.Id == id);
				project.Investor = investor;
				_context.Projects.Update(project);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500); 
            }

            return serviceResponse;
		}


		public async Task<ServiceResponse> AddPartAsync(int id, PartDTO part)
		{
			var response = new ServiceResponse();
            try
            {
				var project = await _query.Include(x => x.Parts).SingleOrDefaultAsync(x => x.Id == id);
				if (project == null)
				{
					response.SetError($"Error: Part '{id}' not found.", 404);
					return response;
				}

				if (project.Parts.SingleOrDefault(x => x.Sign == part.Sign) == null)
				{
					_context.ProjectParts.Add(
						new ProjectPart()
						{
							Project = project,
							Sign = part.Sign,
							Name = part.Name,
						});
				}
                else
                {
                    throw new Exception("Part exist");
                }

				_context.Entry(project).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
            {
                response.SetError($"Error: {ex.Message}", 500);
            }

            return response;
		}


		public async Task<ServiceResponse> UpdatePartAsync(int id, PartDTO part)
		{
			var response = new ServiceResponse();
			try
			{
				var projectPart = await _context.ProjectParts.SingleOrDefaultAsync(x => x.Id == id);
				if (projectPart == null)
				{
					response.SetError($"Error: Part '{id}' not found.", 404);
					return response;
				}

                projectPart.Sign = part.Sign;
                projectPart.Name = part.Name;

				_context.Entry(projectPart).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				response.SetError($"Error: {ex.Message}", 500);
			}

			return response;
		}


		public async Task<ServiceResponse> DeleteProjectPartAsync(int partId)
		{
			var response = new ServiceResponse();
            try
            {
                var part = new ProjectPart { Id = partId };
                _context.Entry(part).State = EntityState.Deleted;
                if (await _context.SaveChangesAsync() == 0)
                {
                    response.SetError($"Error: Part {partId} not found.", 404);
                };
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message, 500);
            }

            return response;
		}
        

        public async Task<ServiceResponse> AddBranchToPartAsync(int partId, int branchId)
		{
            var serviceResponse = new ServiceResponse();
            var part = await _context.PartBranches
                .SingleOrDefaultAsync(x => x.ProjectPartId == partId && x.BranchId == branchId);
            if(part != null)
            {
                serviceResponse.SetError($"Conflict: Branch Id:{branchId} allredy exists.", 409);
                return serviceResponse;
            }
            _context.PartBranches.Add(new PartBranch() 
            { 
                ProjectPartId = partId, 
                BranchId = branchId, 
            });
            await _context.SaveChangesAsync();

            return serviceResponse;
		}

		public async Task<ServiceResponse> DeletePartBranchAsync(int partBranchId)
		{
			var response = new ServiceResponse();
			try
			{
				var branch = await _context.PartBranches.SingleOrDefaultAsync(x => x.Id == partBranchId);
				if (branch == null)
				{
					response.SetError($"Error: Branch Id:{partBranchId} not found.", 404);
					return response;
				}
				_context.PartBranches.Remove(branch);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				response.SetError(ex.Message, 500);
			}

			return response;
		}


		public async Task<ServiceResponse> AddRoleToPartBranchAsync(int partBranchId, TechnicalRole role,  int licenseId)
		{
			var serviceResponse = new ServiceResponse();
            var branch = await _context.PartBranches.SingleOrDefaultAsync(x => x.Id == partBranchId);
            if(branch == null)
            {
                serviceResponse.SetError($"Error: PartBranch '{partBranchId}' not found.");
                return serviceResponse;
            }

            var license = await _context.Licenses.SingleOrDefaultAsync(x => x.Id == licenseId);
            if(license == null)
            {
                serviceResponse.SetError($"Error: License Id:{licenseId} not found.");
				return serviceResponse;
			}

            if(branch.Roles == null)
            {
                branch.Roles = new List<BranchRole>();
            }
            var branchRole = branch.Roles.SingleOrDefault(x =>  x.Role == role);
			if(branchRole != null)
            {
                serviceResponse.SetError($"Conflict: Role '{role}' allready exists.");
                return serviceResponse;
            }

            branch.Roles.Add(new BranchRole
            {
                License= license,
                Role= role,
            });

            await _context.SaveChangesAsync();

			return serviceResponse;
		}


        public async Task<ServiceResponse> DeleteBranchRoleAsync(int branchRoleId)
        {
            var response = new ServiceResponse();
            try
            {
                var role = await _context.BranchRoles.SingleOrDefaultAsync(x => x.Id == branchRoleId);
                if (role == null)
                {
                    response.SetError($"Error: BranchRole Id:{branchRoleId} not found.", 404);
                    return response;
                }
                _context.BranchRoles.Remove(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message, 500);
            }

            return response;
        }


        public async Task<ServiceResponse<ProjectPartDTO>> GetProjectPartWithBranchesAsync(int projectPartId)
		{
            var serviceResponse = new ServiceResponse<ProjectPartDTO>();
            var part = await _context.ProjectParts
                .Include(x => x.Branches)
                .ThenInclude(x => x.Branch)
                .Include(x => x.Branches)
                .ThenInclude(x => x.Roles)
                .ThenInclude(x => x.License)
                .ThenInclude(x => x.Designer)
                .SingleOrDefaultAsync(x => x.Id==projectPartId);
            if(part == null)
            {
                serviceResponse.SetError($"Error: Part Id:'{projectPartId}' not found.");
                return serviceResponse;
            }
            serviceResponse.Data = _mapper.Map<ProjectPartDTO>(part);

            return serviceResponse;
		}

        public async Task<ServiceResponse<PartBranchDTO>> GetPartBranchAsync(int partBranchId)
        {
            var serviceResponse = new ServiceResponse<PartBranchDTO>();
            var part = await _context.PartBranches
                .Include(x => x.Branch)
                .Include(x => x.Roles)
                .ThenInclude(x => x.License)
                .ThenInclude(x => x.Designer)
                .SingleOrDefaultAsync(x => x.Id == partBranchId);
            if (part == null)
            {
                serviceResponse.SetError($"Error: Part Id:'{partBranchId}' not found.");
                return serviceResponse;
            }
            serviceResponse.Data = _mapper.Map<PartBranchDTO>(part);

            return serviceResponse;
        }
    }
}
