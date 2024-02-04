using MediatR;
using SODP.Application.API.Requests.Stages;
using SODP.Application.Extensions;
using SODP.Application.Mappers;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Attributes;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Stages;

public sealed class GetStagesPageHandler : IRequestHandler<GetStagesPageRequest, ApiResponse<Page<StageDTO>>>
{
    private readonly IStageRepository _stageRepository;

	public GetStagesPageHandler(IStageRepository stageRepository)
    {
        _stageRepository = stageRepository;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<Page<StageDTO>>> Handle(
        GetStagesPageRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new StagesSearchSpecification(
            request.ActiveStatus, 
            request.SearchString);

        var page = await _stageRepository
            .Get(specification)
            .Select(x => x.ToDTO())
            .AsPageAsync(request.PageNumber,request.PageSize,cancellationToken);

        return ApiResponse.Success(page);
	}
}
