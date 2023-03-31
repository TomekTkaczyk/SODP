using SODP.Application.Abstractions;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Stages;

public class GetStageByIdQueryHandler : IQueryHandler<GetStageByIdQuery, StageDTO>
{
	private readonly IStageRepository _stageRepository;

	public GetStageByIdQueryHandler(IStageRepository stageRepository)
    {
		_stageRepository = stageRepository;
	}

    public async Task<ApiResponse<StageDTO>> Handle(
		GetStageByIdQuery request, 
		CancellationToken cancellationToken)
	{
		var stage = await _stageRepository.GetByIdAsync(request.Id, cancellationToken);

		if(stage is null)
		{
			return ApiResponse.Failure<StageDTO>(new Error(
				"Stage.NotFound",
				$"The stage with Id:{request.Id} was not found."));
		}
		
		var response = new StageDTO { Id = stage.Id, Name = stage.Name, ActiveStatus = stage.ActiveStatus };

		return ApiResponse.Success(response);
	}
}
