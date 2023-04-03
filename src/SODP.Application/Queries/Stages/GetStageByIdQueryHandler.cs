using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Stages;

public class GetStageByIdQueryHandler : IQueryHandler<GetStageByIdQuery, StageDTO>
{
	private readonly IStageRepository _stageRepository;
	private readonly IMapper _mapper;

	public GetStageByIdQueryHandler(
		IStageRepository stageRepository,
		IMapper mapper)
    {
		_stageRepository = stageRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<StageDTO>> Handle(
		GetStageByIdQuery request, 
		CancellationToken cancellationToken)
	{
		var stage = await _stageRepository
			.ApplySpecyfication(new StageByIdSpecyfication(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if(stage is null)
		{
			var error = new Error("Stage.NotFound", $"The stage with Id:{request.Id} was not found.", HttpStatusCode.NotFound);
			return ApiResponse.Failure<StageDTO>(error, HttpStatusCode.NotFound);
		}

		return ApiResponse.Success(_mapper.Map<StageDTO>(stage));
	}
}
