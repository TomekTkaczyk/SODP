using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Stages;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Stages;

public class GetStageByIdHandler : IRequestHandler<GetStageRequest, ApiResponse<StageDTO>>
{
    private readonly IStageRepository _stageRepository;
	private readonly IMapper _mapper;

	public GetStageByIdHandler(
        IStageRepository stageRepository,
        IMapper mapper)
    {
        _stageRepository = stageRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<StageDTO>> Handle(
        GetStageRequest request,
        CancellationToken cancellationToken)
    {
        var stage = await _stageRepository
            .ApplySpecyfication(new ByIdSpecification<Stage>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("Stage");

        return ApiResponse.Success(_mapper.Map<StageDTO>(stage));
    }
}
