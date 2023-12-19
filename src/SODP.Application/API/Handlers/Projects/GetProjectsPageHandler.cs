using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Attributes;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public sealed class GetProjectsPageHandler : IRequestHandler<GetProjectsPageRequest, ApiResponse<Page<ProjectDTO>>>
{
    private readonly IProjectRepository _projectRepository;
	private readonly IMapper _mapper;

	public GetProjectsPageHandler(
        IProjectRepository projectRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<Page<ProjectDTO>>> Handle(
        GetProjectsPageRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new ProjectsSearchSpecyfication(
            request.Status, 
            request.SearchString);

		var page = await _projectRepository
            .GetPageAsync(
                specification,
                request.PageNumber, 
                request.PageSize, 
                cancellationToken);

		return ApiResponse.Success(_mapper.Map<Page<ProjectDTO>>(page));
	}
}
