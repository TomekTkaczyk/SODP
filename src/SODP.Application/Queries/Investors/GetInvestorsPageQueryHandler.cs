using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Investors;

public sealed class GetInvestorsPageQueryHandler : IQueryHandler<GetInvestorsPageQuery, Page<InvestorDTO>>
{
	private readonly IInvestorRepository _investorRepository;
	private readonly IMapper _mapper;

	public GetInvestorsPageQueryHandler(
		IInvestorRepository investorRepository, 
		IMapper mapper)
    {
		_investorRepository = investorRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<Page<InvestorDTO>>> Handle(
		GetInvestorsPageQuery request, 
		CancellationToken cancellationToken)
	{
		var queryable = _investorRepository
			.ApplySpecyfication(new InvestorByNameSpecification(request.ActiveStatus, request.SearchString));

		var totalItems = await queryable.CountAsync(cancellationToken);
		
		if(request.PageSize > 0)
		{
			queryable = _investorRepository.GetPageQuery(queryable, request.PageNumber, request.PageSize);
		}

		var collection = await queryable.ToListAsync(cancellationToken);

		return ApiResponse.Success(
			Page<InvestorDTO>.Create(
				_mapper.Map<IReadOnlyCollection<InvestorDTO>>(collection),
				request.PageNumber,
				request.PageSize,
				totalItems));
	}
}
