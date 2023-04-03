using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Extensions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Stages;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Stages;

public sealed class GetStagesPageQueryHandler : IQueryHandler<GetStagesPageQuery, Page<StageDTO>>
{
	private readonly IStageRepository _stageRepository;
	private readonly IMapper _mapper;

	public GetStagesPageQueryHandler(
		IStageRepository stageRepository, 
		IMapper mapper)
    {
		_stageRepository = stageRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<Page<StageDTO>>> Handle(
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

		var collection = await queryable.ToListAsync(cancellationToken);

		return ApiResponse.Success(
			Page<StageDTO>.Create(
				_mapper.Map<IReadOnlyCollection<StageDTO>>(collection),
				request.PageNumber,
				request.PageSize,
				totalCount));
	}
}
