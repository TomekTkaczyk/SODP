using AutoMapper;
using SODP.Application.Abstractions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Investors;

public sealed class GetInvestorsPageQueryHandler : IQueryHandler<GetInvestorsPageQuery, Page<InvestorDTO>>
{
	private readonly IInvestorRepository _investorRepository;
	private readonly IMapper _mapper;

	public GetInvestorsPageQueryHandler(IInvestorRepository investorRepository, IMapper mapper)
    {
		_investorRepository = investorRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<Page<InvestorDTO>>> Handle(
		GetInvestorsPageQuery request, 
		CancellationToken cancellationToken)
	{
		var investorsPage = await _investorRepository.GetPageAsync(
			request.ActiveStatus,
			request.SearchString,
			request.PageNumber,
			request.PageSize,
			cancellationToken);

		return ApiResponse.Success(
			Page<InvestorDTO>.Create(
				_mapper.Map<IReadOnlyCollection<InvestorDTO>>(investorsPage.Collection),
				investorsPage.PageNumber, 
				investorsPage.PageSize, 
				investorsPage.TotalCount));
	}
}
