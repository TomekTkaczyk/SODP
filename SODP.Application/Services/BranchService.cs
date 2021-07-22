using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Helpers;
using SODP.Domain.Services;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Branch> _validator;
        private readonly SODPDBContext _context;

        public BranchService(IMapper mapper, IValidator<Branch> validator, SODPDBContext context)
        {
            _mapper = mapper;
            _validator = validator;
            _context = context;
        }

        public async Task<ServicePageResponse<BranchDTO>> GetAllAsync()
        {
            return await GetAllAsync(1, 0, null);
        }

        public async Task<ServicePageResponse<BranchDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0)
        {
            return await GetAllAsync(currentPage, pageSize, null);
        }

        public async Task<ServicePageResponse<BranchDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, bool? active = null)
        {
            var serviceResponse = new ServicePageResponse<BranchDTO>();

            try
            {
                var branches = _context.Branches
                    .Where(x => active == null || (x.ActiveStatus == active))
                    .OrderBy(x => x.Sign);

                serviceResponse.Data.TotalCount = await branches.CountAsync();

                if (pageSize == 0)
                {
                    currentPage = 1;
                    pageSize = serviceResponse.Data.TotalCount;
                }
                else
                {
                    currentPage = Math.Min(currentPage, (int)Math.Ceiling(decimal.Divide(serviceResponse.Data.TotalCount, pageSize)));
                }

                var br = await branches
                    .Skip((currentPage-1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                serviceResponse.Data.PageNumber = currentPage;
                serviceResponse.Data.PageSize = pageSize;
                serviceResponse.SetData(_mapper.Map<IList<BranchDTO>>(branches));

            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<BranchDTO>> GetAsync(int id)
        {
            var serviceResponse = new ServiceResponse<BranchDTO>();
            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == id);
                if(branch == null)
                {
                    serviceResponse.SetError($"Błąd: Branża Id:{id} nie odnaleziona.", 401);
                    return serviceResponse;
                }
                serviceResponse.SetData(_mapper.Map<BranchDTO>(branch));
            }                                                                 
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<BranchDTO>> GetAsync(string sign)
        {
            var serviceResponse = new ServiceResponse<BranchDTO>();
            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Sign == sign);
                if (branch == null)
                {
                    serviceResponse.SetError($"Błąd: Branża {sign} nie odnaleziona.", 401);
                    return serviceResponse;
                }
                serviceResponse.SetData(_mapper.Map<BranchDTO>(branch));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<BranchDTO>> CreateAsync(BranchDTO newBranch)
        {
            var serviceResponse = new ServiceResponse<BranchDTO>();
            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Sign == newBranch.Sign);
                if(branch != null)
                {
                    serviceResponse.SetError($"Błąd: Branża {newBranch.Sign} już istnieje.", 400);
                    return serviceResponse;
                }

                branch = _mapper.Map<Branch>(newBranch);
                var validationResult = await _validator.ValidateAsync(branch);
                if (!validationResult.IsValid)
                {
                    serviceResponse.ValidationErrorProcess(validationResult);
                    return serviceResponse;
                }

                branch.Normalize();
                branch.ActiveStatus = true;
                var entity = await _context.Branches.AddAsync(branch);
                await _context.SaveChangesAsync();
                serviceResponse.SetData(_mapper.Map<BranchDTO>(entity.Entity));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> UpdateAsync(BranchDTO updateBranch)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == updateBranch.Id);
                if (branch == null)
                {
                    serviceResponse.SetError($"Stadium {updateBranch.Id} nie odnalezione.", 404);
                    serviceResponse.ValidationErrors.Add("Sign", "Stadium nie odnalezione.");
                    return serviceResponse;
                }
                branch.Title = updateBranch.Title;
                branch.Normalize();
                _context.Branches.Update(branch);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var serviceResponse = new ServiceResponse();

            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.Id == id);
                if(branch == null)
                {
                    serviceResponse.SetError($"Błąd: Branża Id:{id} nie odnaleziona.", 401);
                    return serviceResponse;
                }

                var projectBranch = await _context.ProjectBranches.FirstOrDefaultAsync(x => x.BranchId == id);
                if(projectBranch != null)
                {
                    serviceResponse.SetError($"Błąd: Branża {projectBranch.Branch.Sign} posiada powiązane projekty.", 400);
                    return serviceResponse;
                }

                _context.Entry(branch).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

    }
}
