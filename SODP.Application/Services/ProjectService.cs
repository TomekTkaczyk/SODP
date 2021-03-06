using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Shared.DTO;
using SODP.Domain.Helpers;
using SODP.Domain.Managers;
using SODP.Domain.Services;
using SODP.Model;
using SODP.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SODP.Shared.Response;
using SODP.Application.Mappers;
using SODP.Shared.Enums;

namespace SODP.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IFolderManager _folderManager;
        private readonly IValidator<Project> _validator;
        private readonly SODPDBContext _context;
        private ProjectStatus _mode = ProjectStatus.Active;

        public ProjectService(IMapper mapper, IFolderManager folderManager, IValidator<Project> validator, SODPDBContext context)
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

        public async Task<ServicePageResponse<ProjectDTO>> GetAllAsync()
        {
            return await GetAllAsync(1, 0);
        }   

        public async Task<ServicePageResponse<ProjectDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            var serviceResponse = new ServicePageResponse<ProjectDTO>();
            IList<Project> projects = new List<Project>();

            serviceResponse.Data.TotalCount = await _context.Projects
                .Where(x => x.Status == _mode && (string.IsNullOrEmpty(searchString) || x.Title.Contains(searchString)))
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
                    .Where(x => x.Status == _mode && (string.IsNullOrEmpty(searchString) || x.Title.Contains(searchString)))
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
                    .Where(t => t.Status == _mode)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    serviceResponse.SetError($"Błąd: Projekt Id:{id} nie odnaleziony.", 404);
                }
                serviceResponse.SetData(project.ToDTO());
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
                    serviceResponse.SetError($"Error: Project {exist.Symbol} exist.", 400);
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
                project.Address = "";
                project.Investment = "";
                project.Investor = "";
                project.TitleStudy = "";
                var (Success, Message) = await _folderManager.CreateFolderAsync(project);
                if (!Success)
                {
                    throw new ApplicationException($"Błąd: {Message}");
                }

                project.Location = newProject.ToString();
                var entity = await _context.AddAsync(project);
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

                oldProject.Title = project.Title;
                oldProject.Description = project.Description;
                oldProject.TitleStudy = project.TitleStudy;
                oldProject.Investment = project.Investment;
                oldProject.Address = project.Address;
                oldProject.Investor = project.Investor;
                oldProject.Location = project.ToString();
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
                _context.SaveChanges();

                var (Success, Message) = await _folderManager.ArchiveFolderAsync(project);
                if (!Success)
                {
                    project.Status = ProjectStatus.Active;
                    _context.SaveChanges();
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
                _context.Entry(project).State = EntityState.Deleted;
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
                _context.SaveChanges();
                
                var (Success, Message) = await _folderManager.RestoreFolderAsync(project);
                if (!Success)
                {
                    throw new ApplicationException($"Błąd: {Message}");
                }
                project.Status = ProjectStatus.Active;
                _context.Entry(project).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
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
                    serviceResponse.SetError($"Błąd: Branża Id:{branchId} nie odnaleziona.", 404);
                    return serviceResponse;
                }

                var projectBranch = await _context.ProjectBranches.FirstOrDefaultAsync(x => x.ProjectId == id && x.BranchId == branchId);
                if (projectBranch == null)
                {
                    projectBranch = new ProjectBranch
                    {
                        ProjectId = id,
                        BranchId = branchId
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
                    _context.Entry(projectBranch).State = EntityState.Deleted;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message, 500);
            }

            return serviceResponse;
        }
    }
}
