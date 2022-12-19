using AutoMapper;
using FluentValidation;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Threading.Tasks;

namespace SODP.Infrastructure.Services
{
	public class InvestorService : AppService<Investor, InvestorDTO>, IInvestorService
	{
		public InvestorService(IMapper mapper, IValidator<Investor> validator, SODPDBContext context, IActiveStatusService<Investor> activeStatusService) : base(mapper, validator, context, activeStatusService)
		{
		}

		public Task<ServiceResponse<InvestorDTO>> CreateAsync(InvestorDTO entity)
		{
			throw new NotImplementedException();
		}

		public Task<int> GetAsync(InvestorDTO designer)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResponse> UpdateAsync(InvestorDTO entity)
		{
			throw new NotImplementedException();
		}
	}
}
