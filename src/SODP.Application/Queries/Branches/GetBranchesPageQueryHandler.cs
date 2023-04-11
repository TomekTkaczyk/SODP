using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Branches;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Branches;

public sealed class GetBranchesPageQueryHandler : IRequestHandler<GetBranchesPageQuery, Page<Branch>>
{
	private readonly IBranchRepository _branchRepository;

	public GetBranchesPageQueryHandler(
 		IBranchRepository branchRepository)
	{
		_branchRepository = branchRepository;
	}

	public async Task<Page<Branch>> Handle(
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

		var collection = new ReadOnlyCollection<Branch>(await queryable.ToListAsync(cancellationToken));

		return Page<Branch>.Create(
				collection,
				request.PageNumber,
				request.PageSize,
				totalItems);
	}
}
