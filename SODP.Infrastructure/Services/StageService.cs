﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Domain.Helpers;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Services
{
    public class StageService : FilteredPageService<Stage, StageDTO>, IStageService
    {

        public StageService(IMapper mapper, IValidator<Stage> validator, SODPDBContext context, IActiveStatusService<Stage> activeStatusService) : base(mapper, validator, context, activeStatusService) 
        {
            _query = _query.OrderBy(x => x.Name);
        }


        public async Task<ServiceResponse<StageDTO>> GetAsync(string sign)
        {
            var serviceResponse = new ServiceResponse<StageDTO>();
            try
            {
                var stage = await _context.Stages.SingleOrDefaultAsync(x => x.Sign == sign);

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
                var stage = await _context.Stages.SingleOrDefaultAsync(x => x.Sign.ToUpper() == newStage.Sign.ToUpper());
                if (stage != null)
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
                var entity = _context.Stages.Add(stage);
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
                var stage = await _context.Stages.SingleOrDefaultAsync(x => x.Id == updateStage.Id);
                if (stage == null)
                {
                    serviceResponse.SetError($"Stadium {updateStage.Id} nie odnalezione.", 404);
                    serviceResponse.ValidationErrors.Add("Sign", "Stadium nie odnalezione.");
                    return serviceResponse;
                }
                stage.Name = updateStage.Name;
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


        public override async Task<ServiceResponse> DeleteAsync(int id)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var project = await _context.Projects
                    .Include(x => x.Stage)
                    .SingleOrDefaultAsync(x => x.Stage.Id == id);
                if (project != null)
                {
					serviceResponse.SetError($"Error: Stage '{project.Stage.Sign}' has subsidiary projects.", 400);
                    
                    return serviceResponse;
                }
                
                serviceResponse = await base.DeleteAsync(id);
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
                var stage = await _context.Stages.SingleOrDefaultAsync(x => x.Sign == sign);
                if (stage == null)
                {
                    serviceResponse.SetError($"Stadium Sign:{sign} nie odnalezione.", 404);
                    serviceResponse.ValidationErrors.Add("Sign", $"Stadium {sign} nie odnalezione.");
                    return serviceResponse;
                }
                var project = await _context.Projects.SingleOrDefaultAsync(x => x.Stage.Sign == sign);
                if (project != null)
                {
                    serviceResponse.SetError($"Stadium {sign} posiada powiązane projekty.", 409);
                    serviceResponse.ValidationErrors.Add("Sign", $"Stadium {sign} posiada powiązane projekty.");
                    return serviceResponse;
                }
                _context.Stages.Remove(stage);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }


        public async Task<bool> ExistAsync(string sign)
        {
            return ( await _context.Stages.SingleOrDefaultAsync(x => x.Sign == sign) != null);
        }

        public override async Task<ServicePageResponse<StageDTO>> GetPageAsync(bool? active, string searchString, int currentPage = 1, int pageSize = 0)
        {
            _query = _context.Stages
                .Where(x => !active.HasValue || x.ActiveStatus.Value.Equals(active))
                .Where(x => string.IsNullOrWhiteSpace(searchString) || x.Sign.Contains(searchString) || x.Name.Contains(searchString))
                .OrderBy(x => x.Order)
                .ThenBy(x => x.Sign);

            return await GetPageAsync(currentPage, pageSize);
        }
    }
}
