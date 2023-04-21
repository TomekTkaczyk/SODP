using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Stages;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Stages;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Stages;

public sealed class GetStagesPageHandler : IRequestHandler<GetStagesPageRequest, ApiResponse<Page<StageDTO>>>
{
    private readonly IStageRepository _stageRepository;
	private readonly IMapper _mapper;

	public GetStagesPageHandler(
        IStageRepository stageRepository,
        IMapper mapper)
    {
        _stageRepository = stageRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<Page<StageDTO>>> Handle(
        GetStagesPageRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new StageByNameSpecification(request.ActiveStatus, request.SearchString);
        var page = await _stageRepository.GetPage(specification, request.PageNumber, request.PageSize, cancellationToken);

        return ApiResponse.Success(_mapper.Map<Page<StageDTO>>(page));
    }
}
