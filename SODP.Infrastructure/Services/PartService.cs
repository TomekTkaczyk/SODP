using AutoMapper;
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
    public class PartService : FilteredPageService<Part, PartDTO>, IPartService
    {

		public PartService(IMapper mapper, IValidator<Part> validator, SODPDBContext context, IActiveStatusService<Part> activeStatusService) : base(mapper, validator, context, activeStatusService) { }


        public async Task<ServiceResponse<PartDTO>> CreateAsync(PartDTO newPart)
        {
            var serviceResponse = new ServiceResponse<PartDTO>();
            try
            {
                var part = await _context.Parts.FirstOrDefaultAsync(x => x.Sign == newPart.Sign);
                if (part != null)
                {
                    serviceResponse.SetError($"Error: Part '{newPart.Sign}' exist.", 409);
                    return serviceResponse;
                }

                part = _mapper.Map<Part>(newPart);
                var validationResult = await _validator.ValidateAsync(part);
                if (!validationResult.IsValid)
                {
                    serviceResponse.ValidationErrorProcess(validationResult);
                    return serviceResponse;
                }

                part.Normalize();
                part.ActiveStatus = true;
                var entity = await _context.Parts.AddAsync(part);
                await _context.SaveChangesAsync();
                serviceResponse.SetData(_mapper.Map<PartDTO>(entity.Entity));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse> UpdateAsync(PartDTO updatePart)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var part = await _query.FirstOrDefaultAsync(x => x.Id != updatePart.Id && x.Sign.Equals(updatePart.Sign));
                if (part != null)
                {
                    serviceResponse.SetError("Part exist.", 409);
                    return serviceResponse;
                }

				part = await _query.FirstOrDefaultAsync(x => x.Id == updatePart.Id);
                if (part == null)
                {
                    serviceResponse.SetError($"Part '{updatePart.Id}' not found.", 404);
                    return serviceResponse;
                }
				part.Sign = updatePart.Sign;
				part.Name = updatePart.Name;
				part.Normalize();
                _context.Entry(part).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }
            return serviceResponse;
        }


        public async Task<ServiceResponse<PartDTO>> GetAsync(string sign)
        {
            var serviceResponse = new ServiceResponse<PartDTO>();
            try
            {
                var part = await _query.FirstOrDefaultAsync(x => x.Sign == sign);
                if (part == null)
                {
                    serviceResponse.SetError($"Error: Part '{sign}' not found.", 404);
                    return serviceResponse;
                }
                serviceResponse.SetData(_mapper.Map<PartDTO>(part));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }


        protected override FilteredPageService<Part, PartDTO> WithSearchString(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                _query = _query.Where(x => x.Sign.Contains(searchString) || x.Name.Contains(searchString));
            }

            return this;
        }


    }
}
