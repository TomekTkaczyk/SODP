using AutoMapper;
using SODP.Application.Abstractions;
using SODP.Application.ValueObjects;
using SODP.Domain.Repositories;
using SODP.Domain.Shared;
using SODP.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace SODP.Application.Queries.Investors;

public sealed class GetInvestorsPageQueryHandler : IQueryHandler<GetInvestorsPageQuery, Page<InvestorValueObject>>
{
	private readonly IInvestorRepository _investorRepository;
	private readonly IMapper _mapper;

	public GetInvestorsPageQueryHandler(IInvestorRepository investorRepository, IMapper mapper)
    {
		_investorRepository = investorRepository;
		_mapper = mapper;
	}

    public async Task<Result<Page<InvestorValueObject>>> Handle(
		GetInvestorsPageQuery request, 
		CancellationToken cancellationToken)
	{
		var investorsPage = await _investorRepository.GetPageAsync(
			request.ActiveStatus,
			request.SearchString,
			request.PageNumber,
			request.PageSize,
			cancellationToken);

		if(investorsPage.Collection.Count == 0)
		{
			return Result.Failure<Page<InvestorValueObject>>(new Error(
				"Investor.NotFound", 
				"List of investors is empty."));
		}

		return Result.Success(
			new Page<InvestorValueObject>(
				_mapper.Map<IReadOnlyCollection<InvestorValueObject>>(investorsPage.Collection),
				investorsPage.PageNumber,
				investorsPage.PageSize,
				investorsPage.TotalCount));
	}
}
