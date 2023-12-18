using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Attributes;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetProjectWithDetailsHandler : IRequestHandler<GetProjectWithDetailsRequest, ApiResponse<ProjectDTO>>
{
    private readonly IProjectRepository _projectRepository;
	private readonly IMapper _mapper;

	public GetProjectWithDetailsHandler(
        IProjectRepository projectRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<ProjectDTO>> Handle(
        GetProjectWithDetailsRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetWithDetailsAsync(request.Id, cancellationToken)
			?? throw new NotFoundException("Project");

        return ApiResponse.Success(_mapper.Map<ProjectDTO>(project));
    }
}
