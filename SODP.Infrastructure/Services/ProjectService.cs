﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Interfaces;
using SODP.Domain.Helpers;
using SODP.Domain.Managers;
using SODP.Model;
using SODP.Model.Enums;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IFolderManager _folderManager;
        private readonly IValidator<Project> _validator;
        private readonly ISODPDBContext _context;
        private ProjectStatus _mode = ProjectStatus.Active;

        public ProjectService(IMapper mapper, IFolderManager folderManager, IValidator<Project> validator, ISODPDBContext context)
        {
            _mapper = mapper;
            _folderManager = folderManager;
            _validator = validator;
            _context = context;
        }

        public IProjectService SetActiveMode()
        {
            _mode = ProjectStatus.Active;

            return this;
        }

        public IProjectService SetArchiveMode()
        {
            _mode = ProjectStatus.Archived;

            return this;
        }

        public async Task<ServicePageResponse<ProjectDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            var serviceResponse = new ServicePageResponse<ProjectDTO>();
            IList<Project> projects = new List<Project>();

            serviceResponse.Data.TotalCount = await _context.Projects
                .Where(x => x.Status == _mode && (string.IsNullOrEmpty(searchString) || x.Name.Contains(searchString)))
                .CountAsync();
            if (pageSize == 0)
            {
                pageSize = serviceResponse.Data.TotalCount;
            }

            try
            {
                projects = await _context.Projects.Include(s => s.Stage)
                    .OrderBy(x => x.Number)
                    .ThenBy(y => y.Stage.Sign)
                    .Where(x => x.Status == _mode && (string.IsNullOrEmpty(searchString) || x.Name.Contains(searchString) || x.Title.Contains(searchString)))
                    .Skip((currentPage-1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                serviceResponse.Data.PageNumber = currentPage;
                serviceResponse.Data.PageSize = pageSize;
                serviceResponse.SetData(_mapper.Map<IList<ProjectDTO>>(projects));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ProjectDTO>> GetAsync(int id)
        {
            var serviceResponse = new ServiceResponse<ProjectDTO>();
            try
            {
                var project = await _context.Projects
                    .Include(s => s.Stage)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    serviceResponse.SetError($"Error: Project Id:{id} not found.", 404);
                } else
                {
                    serviceResponse.SetData(_mapper.Map<ProjectDTO>(project));
                }
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ProjectDTO>> GetWithBranchesAsync(int id)
        {
            var serviceResponse = new ServiceResponse<ProjectDTO>();
            try
            {
                var project = await _context.Projects
                    .Include(s => s.Stage)
                    .Include(s => s.Branches)
                    .ThenInclude(s => s.Branch)
                    .Include(s => s.Branches)
                    .ThenInclude(s => s.Roles)
                    .ThenInclude(s => s.License)
                    .ThenInclude(s => s.Designer)
                    .Where(t => t.Status == _mode)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    serviceResponse.SetError($"Błąd: Projekt Id:{id} nie odnaleziony.", 404);
                }
                serviceResponse.SetData(_mapper.Map<ProjectDTO>(project));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ProjectDTO>> CreateAsync(NewProjectDTO newProject)
        {
            var serviceResponse = new ServiceResponse<ProjectDTO>();
            try
            {
                var exist = await _context.Projects.Include(x => x.Stage).FirstOrDefaultAsync(x => x.Number == newProject.Number && x.Stage.Id == newProject.StageId);
                if(exist != null)
                {
                    serviceResponse.SetError($"Błąd: Projekt {exist.Symbol} już istnieje.", 400);
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
                project.Stage = await _context.Stages.FirstOrDefaultAsync(x => x.Id == project.StageId);
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

                var entity =_context.Projects.Add(project);
                await _context.SaveChangesAsync();
                serviceResponse.SetData(_mapper.Map<ProjectDTO>(entity.Entity));
            }
            catch(Exception ex)
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
                var oldProject = await _context.Projects.Include(x => x.Stage).FirstOrDefaultAsync(x => x.Id == updateProject.Id);
                if(oldProject == null)
                {
                    serviceResponse.SetError($"Błąd: Project Id:{updateProject.Id} nie odnaleziony.", 404);
                    return serviceResponse;
                }
                var project = _mapper.Map<Project>(updateProject);
                project.Stage = await _context.Stages.FirstOrDefaultAsync(x => x.Id == project.StageId);
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
                var project = await _context.Projects.Include(x => x.Stage).FirstOrDefaultAsync(x => x.Id == id);
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
                
                project.Status = ProjectStatus.Archived;
                project.ModifyTimeStamp = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var project = await _context.Projects.Include(x => x.Stage).FirstOrDefaultAsync(x => x.Id == id); 
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
                var project = await _context.Projects.Include(x => x.Stage).FirstOrDefaultAsync(x => x.Id == id); 
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

        public async Task<ServicePageResponse<ProjectBranchRoleDTO>> GetBranchRolesAsync(int id, int branchId)
        {
            var serviceResponse = new ServicePageResponse<ProjectBranchRoleDTO>();
            try 
            {
                var projectBranch = await _context.ProjectBranches
                    .Include(x => x.Roles)
                    .ThenInclude(x => x.License)
                    .ThenInclude(x => x.Designer)
                    .FirstOrDefaultAsync(x => x.Project.Id == id && x.BranchId == branchId);
                var roles = projectBranch.Roles.ToList();
                serviceResponse.SetData(_mapper.Map<IList<ProjectBranchRoleDTO>>(projectBranch.Roles));
            }
            catch(Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> AddBranchAsync(int id, int branchId)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == branchId);
                if (branch == null)
                {
                    serviceResponse.SetError($"Błąd: Branża Id:{branchId} nie odnaleziona", 404);
                    return serviceResponse;
                }

                var project = await _context.Projects.Include(b => b.Branches).FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    serviceResponse.SetError($"Błąd: Projekt Id:{branchId} nie odnaleziony", 404);
                    return serviceResponse;
                }

                if(project.Branches == null || (project.Branches.FirstOrDefault(x => x.BranchId == branchId) == null))
                {
                    var projectBranch = new ProjectBranch
                    {
                        Project = project,
                        Branch = branch
                    };
                    var result = await _context.ProjectBranches.AddAsync(projectBranch);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteBranchAsync(int id, int branchId)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var projectBranch = await _context.ProjectBranches.FirstOrDefaultAsync(x => x.ProjectId == id && x.BranchId == branchId);
                if(projectBranch != null)
                {
                    _context.ProjectBranches.Remove(projectBranch);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> SetBranchTechnicalRoleAsync(TechnicalRoleDTO technicalRole)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var projectBranch = await _context.ProjectBranches
                    .Include(x => x.Roles)
                    .FirstOrDefaultAsync(x => x.ProjectId == technicalRole.ProjectId && x.BranchId == technicalRole.BranchId);
                if (projectBranch == null)
                {
                    serviceResponse.SetError("Branża projektu nie odnaleziona", 400);
                    return serviceResponse;
                }

                var branchRole = projectBranch.Roles.FirstOrDefault(x => x.Role == technicalRole.Role);

                if (technicalRole.LicenseId == 0)
                {
                    if(branchRole != null)
                    {
                        projectBranch.Roles.Remove(branchRole);
                    }
                }
                else
                {
                    var license = await _context.Licenses
                        .Include(x => x.Branches)
                        .FirstOrDefaultAsync(x => x.Id == technicalRole.LicenseId);

                    if (license == null)
                    {
                        serviceResponse.SetError("Uprawnienia nie odnalezione", 400);
                        return serviceResponse;
                    }

                    if (license.Branches == null || license.Branches.FirstOrDefault(x => x.BranchId == technicalRole.BranchId) == null)
                    {
                        serviceResponse.SetError($"Uprawnienia nie obejmują branży", 400);
                        return serviceResponse;
                    }

                    if(branchRole == null)
                    {
                        projectBranch.Roles.Add(new ProjectBranchRole() { License = license, Role = technicalRole.Role });
                    } 
                    else
                    {
                        branchRole.License = license;
                    }
                }
                _context.ProjectBranches.Update(projectBranch);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }
    }
}