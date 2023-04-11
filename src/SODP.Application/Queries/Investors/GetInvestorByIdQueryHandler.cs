using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Investors;

public sealed class GetInvestorByIdQueryHandler : IRequestHandler<GetInvestorByIdQuery, Investor>
{
	private readonly IInvestorRepository _investorRepository;

	public GetInvestorByIdQueryHandler(
		IInvestorRepository investorRepository)
    {
		_investorRepository = investorRepository;
	}

    public async Task<Investor> Handle(
		GetInvestorByIdQuery request, 
		CancellationToken cancellationToken)
	{
		var investor = await _investorRepository
			.ApplySpecyfication(new InvestorByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		return investor ?? throw new NotFoundException("Investor");
	}
}
