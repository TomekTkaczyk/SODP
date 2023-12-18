using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Attributes;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetProjectPartHandler : IRequestHandler<GetProjectPartRequest, ApiResponse<ProjectPartDTO>>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IMapper _mapper;

	public GetProjectPartHandler(
        IProjectRepository projectRepository,
		IMapper mapper)
    {
		_projectRepository = projectRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<ProjectPartDTO>> Handle(GetProjectPartRequest request, CancellationToken cancellationToken)
	{
		var projectPart = await _projectRepository.GetPartAsync(request.ProjectPartId, cancellationToken);


		return ApiResponse.Success(_mapper.Map<ProjectPartDTO>(projectPart));
	}
}
