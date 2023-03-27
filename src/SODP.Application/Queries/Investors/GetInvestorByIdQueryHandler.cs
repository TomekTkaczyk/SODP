using SODP.Application.Abstractions;
using SODP.Application.ValueObjects;
using SODP.Domain.Repositories;
using SODP.Domain.Shared;
using SODP.Domain.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Investors;

public sealed class GetInvestorByIdQueryHandler : IQueryHandler<GetInvestorByIdQuery, InvestorValueObject>
{
	private readonly IInvestorRepository _investorRepository;

	public GetInvestorByIdQueryHandler(IInvestorRepository investorRepository)
    {
		_investorRepository = investorRepository;
	}

    public async Task<Result<InvestorValueObject>> Handle(
		GetInvestorByIdQuery request, 
		CancellationToken cancellationToken)
	{
		var investor = await _investorRepository.GetByIdAsync(request.Id, cancellationToken);

		if (investor is null)
		{
			return Result.Failure<InvestorValueObject>(new Error(
				"Investor.NotFound",
				$"The investor with Id:{request.Id} was not found."));
		}

		var response = new InvestorValueObject(investor.Id, investor.Name, investor.ActiveStatus);

		return Result.Success(response);
	}
}
