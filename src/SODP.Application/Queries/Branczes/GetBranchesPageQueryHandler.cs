using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Branches;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Branczes;

public sealed class GetBranchesPageQueryHandler : IQueryHandler<GetBranchesPageQuery, Page<BranchDTO>>
{
	private readonly IBranchRepository _branchRepository;
	private readonly IMapper _mapper;

	public GetBranchesPageQueryHandler(
 		IBranchRepository branchRepository,
		IMapper mapper)
	{
		_branchRepository = branchRepository;
		_mapper = mapper;
	}

	public async Task<ApiResponse<Page<BranchDTO>>> Handle(
		GetBranchesPageQuery request, 
		CancellationToken cancellationToken)
	{
		var queryable = _branchRepository
			.ApplySpecyfication(new BranchByNameSpecification(request.ActiveStatus, request.SearchString));

		var totalItems = await queryable.CountAsync(cancellationToken);

		if (request.PageSize > 0)
		{
			queryable = _branchRepository.GetPageQuery(queryable, request.PageNumber, request.PageSize);
		}

		var collection = await queryable.ToListAsync(cancellationToken);

		return ApiResponse.Success(
			Page<BranchDTO>.Create(
				_mapper.Map<IReadOnlyCollection<BranchDTO>>(collection),
				request.PageNumber,
				request.PageSize,
				totalItems));
	}
}
