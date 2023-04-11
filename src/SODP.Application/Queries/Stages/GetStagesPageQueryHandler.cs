using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Extensions;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Stages;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Stages;

public sealed class GetStagesPageQueryHandler : IRequestHandler<GetStagesPageQuery, Page<Stage>>
{
	private readonly IStageRepository _stageRepository;

	public GetStagesPageQueryHandler(
		IStageRepository stageRepository)
    {
		_stageRepository = stageRepository;
	}

    public async Task<Page<Stage>> Handle(
		GetStagesPageQuery request, 
		CancellationToken cancellationToken)
	{
		var queryable = _stageRepository
			.ApplySpecyfication(new StageByNameSpecification(request.ActiveStatus, request.SearchString));

		var totalCount = await queryable.CountAsync(cancellationToken);

		if(request.PageSize > 0)
		{
			queryable = _stageRepository.GetPageQuery(queryable, request.PageNumber, request.PageSize);
		}

		var collection = new ReadOnlyCollection<Stage>(await queryable.ToListAsync(cancellationToken));

		return Page<Stage>.Create(
				collection,
				request.PageNumber,
				request.PageSize,
				totalCount);
	}
}
