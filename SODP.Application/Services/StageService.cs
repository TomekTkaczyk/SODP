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
    public class StageService : IStageService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Stage> _validator;
        private readonly SODPDBContext _context;

        public StageService(IMapper mapper, IValidator<Stage> validator, SODPDBContext context)
        {
            _mapper = mapper;
            _validator = validator;
            _context = context;
        }

        public async Task<ServicePageResponse<StageDTO>> GetAllAsync()
        {
            return await GetAllAsync(1, 0, null);
        }

        public async Task<ServicePageResponse<StageDTO>> GetAllAsync(int currentPage = 1, int pageSize = 0, string searchString = "", bool? active = null)
        {
            var serviceResponse = new ServicePageResponse<StageDTO>();
            IList<Stage> projects = new List<Stage>();

            serviceResponse.Data.TotalCount = await _context.Stages
                .Where(x => x.ActiveStatus == active && (string.IsNullOrEmpty(searchString) || x.Title.Contains(searchString)))
                .CountAsync();
            if (pageSize == 0)
            {
                pageSize = serviceResponse.Data.TotalCount;
            }

            try
            {
                var stages = _context.Stages
                    .Where(p => !string.IsNullOrEmpty(p.Sign) && (string.IsNullOrEmpty(searchString) || p.Title.Contains(searchString)))
                    .OrderBy(x => x.Sign);

                serviceResponse.Data.TotalCount = await stages.CountAsync();

                if (pageSize == 0)
                {
                    currentPage = 1;
                    pageSize = serviceResponse.Data.TotalCount;
                }
                else
                {
                    currentPage = Math.Min(currentPage, (int)Math.Ceiling(decimal.Divide(serviceResponse.Data.TotalCount, pageSize)));
                }

                var st = await stages
                    .Skip((currentPage-1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                serviceResponse.Data.TotalCount = stages.Count();
                serviceResponse.Data.PageNumber = currentPage;
                serviceResponse.Data.PageSize = pageSize;
                serviceResponse.SetData(_mapper.Map<IList<StageDTO>>(st));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<StageDTO>> GetAsync(int id)
        {
            var serviceResponse = new ServiceResponse<StageDTO>();
            try
            {
                var stage = await _context.Stages.FirstOrDefaultAsync(x => x.Id == id);
                if (stage == null)
                {
                    serviceResponse.SetError($"Błąd: Stadium Id:{id} nie odnalezione.", 401);
                    return serviceResponse;
                }
                serviceResponse.SetData(_mapper.Map<StageDTO>(stage));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<StageDTO>> GetAsync(string sign)
        {
            var serviceResponse = new ServiceResponse<StageDTO>();
            try
            {
                var stage = await _context.Stages.FirstOrDefaultAsync(x => x.Sign == sign);

                serviceResponse.SetData(_mapper.Map<StageDTO>(stage));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<StageDTO>> CreateAsync(StageDTO newStage)
        {
            var serviceResponse = new ServiceResponse<StageDTO>();
            try
            {
                var stage = await _context.Stages.FirstOrDefaultAsync(x => x.Sign.ToUpper() == newStage.Sign.ToUpper());
                if(stage != null)
                {

                    serviceResponse.SetError($"Stadium {newStage.Sign.ToUpper()} już istnieje.", 400);
                    serviceResponse.ValidationErrors.Add("Sign", "Stadium już istnieje.");
                    return serviceResponse;
                }

                stage = _mapper.Map<Stage>(newStage);
                var validationResult = await _validator.ValidateAsync(stage);
                if (!validationResult.IsValid)
                {
                    serviceResponse.ValidationErrorProcess(validationResult);
                    return serviceResponse;
                }

                stage.Normalize();
                stage.ActiveStatus = true;
                var entity = await _context.AddAsync(stage);
                await _context.SaveChangesAsync();

                serviceResponse.SetData(_mapper.Map<StageDTO>(entity.Entity));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> UpdateAsync(StageDTO updateStage)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var stage = await _context.Stages.FirstOrDefaultAsync(x => x.Id == updateStage.Id);
                if(stage == null)
                {
                    serviceResponse.SetError($"Stadium {updateStage.Id} nie odnalezione.", 404);
                    serviceResponse.ValidationErrors.Add("Sign", "Stadium nie odnalezione.");
                    return serviceResponse;
                }
                stage.Title = updateStage.Title;
                stage.Normalize();
                _context.Stages.Update(stage);
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
                var stage = await _context.Stages.FirstOrDefaultAsync(x => x.Id == id);
                if (stage == null)
                {
                    serviceResponse.SetError($"Stadium [{id}] nie odnalezione.", 404);
                    serviceResponse.ValidationErrors.Add("Id", $"Stadium [{id}] nie odnalezione.");

                    return serviceResponse;
                }
                var project = await _context.Projects.FirstOrDefaultAsync(x => x.Stage.Id == id);
                if(project != null)
                {
                    serviceResponse.SetError($"Stadium {project.Stage.Sign} posiada powiązane projekty.", 400);
                    serviceResponse.ValidationErrors.Add("Id", $"Stadium [{id}] posiada powiązane projekty.");
                    return serviceResponse;
                }
                _context.Entry(stage).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteAsync(string sign)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var stage = await _context.Stages.FirstOrDefaultAsync(x => x.Sign == sign);
                if (stage == null)
                {
                    serviceResponse.SetError($"Stadium Sign:{sign} nie odnalezione.", 404);
                    serviceResponse.ValidationErrors.Add("Sign", $"Stadium {sign} nie odnalezione.");
                    return serviceResponse;
                }
                var project = await _context.Projects.FirstOrDefaultAsync(x => x.Stage.Sign == sign);
                if(project != null)
                {
                    serviceResponse.SetError($"Stadium {sign} posiada powiązane projekty.", 409);
                    serviceResponse.ValidationErrors.Add("Sign", $"Stadium {sign} posiada powiązane projekty.");
                    return serviceResponse;
                }
                _context.Entry(stage).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
                return serviceResponse;
            }

            return serviceResponse;
        }

        public async Task<bool> ExistAsync(string sign)
        {
            var result = await _context.Stages.FirstOrDefaultAsync(x => x.Sign == sign);

            return result != null;
        }

        public async Task<bool> ExistAsync(int id)
        {
            var result = await _context.Stages.FirstOrDefaultAsync(x => x.Id == id);

            return result != null;
        }
    }
}
