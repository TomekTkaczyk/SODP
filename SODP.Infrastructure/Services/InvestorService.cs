﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Services
{
	public class InvestorService : FilteredPageService<Investor, InvestorDTO>, IInvestorService
	{
		public InvestorService(IMapper mapper, IValidator<Investor> validator, SODPDBContext context, IActiveStatusService<Investor> activeStatusService) : base(mapper, validator, context, activeStatusService) { }

        public async Task<ServiceResponse<InvestorDTO>> CreateAsync(InvestorDTO newInvestor)
		{
			var serviceResponse = new ServiceResponse<InvestorDTO>();
			try
			{
				var investor = _mapper.Map<Investor>(newInvestor);
				investor.ActiveStatus = true;
				var entity = await _context.Investors.AddAsync(investor);
				await _context.SaveChangesAsync();
				serviceResponse.SetData(_mapper.Map<InvestorDTO>(entity.Entity));
			}
			catch (Exception ex)
			{
				serviceResponse.SetError(ex.Message);
			}

			return serviceResponse;
		}

        public async Task<ServiceResponse> UpdateAsync(InvestorDTO updateInvestor)
		{
			var serviceResponse = new ServiceResponse();
			try
			{
				var investor = await _context.Investors.SingleOrDefaultAsync(x => x.Id != updateInvestor.Id && x.Name == updateInvestor.Name);
				if (investor != null)
				{
					serviceResponse.SetError("Inwestor już istnieje", 409);
					serviceResponse.ValidationErrors.Add("Name", "Inwestor już istnieje w bazie.");

					return serviceResponse;
				}

				investor = await _context.Investors.SingleOrDefaultAsync(x => x.Id == updateInvestor.Id);
				if (investor == null)
				{
					serviceResponse.SetError($"Inwestor {investor.Id} nie odnaleziony.", 404);
					serviceResponse.ValidationErrors.Add("Sign", "Inwestor nie odnaleziony.");
					return serviceResponse;
				}
				investor.Name = updateInvestor.Name;
				_context.Investors.Update(investor);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				serviceResponse.SetError(ex.Message);
			}
			return serviceResponse;
		}

        public override async Task<ServicePageResponse<InvestorDTO>> GetPageAsync(bool? active, string searchString, int currentPage = 1, int pageSize = 0)
        {
			_query = _context.Investors
				.Where(x => !active.HasValue || x.ActiveStatus.Value.Equals(active))
				.Where(x => string.IsNullOrWhiteSpace(searchString) || x.Name.Contains(searchString))
				.OrderBy(x => x.Name)
				.ThenBy(x => x.Id);

			return await GetPageAsync(currentPage, pageSize);
        }
    }
}
