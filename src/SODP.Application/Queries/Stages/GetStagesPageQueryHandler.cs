using AutoMapper;
using SODP.Application.Abstractions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Stages;

public sealed class GetStagesPageQueryHandler : IQueryHandler<GetStagesPageQuery, Page<StageDTO>>
{
	private readonly IStageRepository _stageRepository;
	private readonly IMapper _mapper;

	public GetStagesPageQueryHandler(IStageRepository stageRepository, IMapper mapper)
    {
		_stageRepository = stageRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<Page<StageDTO>>> Handle(
		GetStagesPageQuery request, 
		CancellationToken cancellationToken)
	{
		var investorsPage = await _stageRepository.GetPageAsync(
			request.ActiveStatus,
			request.SearchString,
			request.PageNumber,
			request.PageSize,
			cancellationToken);

		return ApiResponse.Success(
			Page<StageDTO>.Create(
				_mapper.Map<IReadOnlyCollection<StageDTO>>(investorsPage.Collection),
				investorsPage.PageNumber,
				investorsPage.PageSize,
				investorsPage.TotalCount));
	}
}
