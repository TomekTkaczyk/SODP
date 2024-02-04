using MediatR;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Mappers;
using SODP.Domain.Attributes;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetProjectDetailsHandler : IRequestHandler<GetProjectDetailsRequest, ApiResponse<ProjectDTO>>
{
    private readonly IProjectRepository _projectRepository;

	public GetProjectDetailsHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<ProjectDTO>> Handle(
        GetProjectDetailsRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetDetailsAsync(request.Id, cancellationToken)
			?? throw new ProjectNotFoundException();

        var projectDTO = project.ToDTO();

        return ApiResponse.Success(projectDTO);
    }
}
