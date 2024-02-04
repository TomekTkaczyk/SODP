using MediatR;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Extensions;
using SODP.Application.Mappers;
using SODP.Domain.Attributes;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public sealed class GetProjectsPageHandler : IRequestHandler<GetProjectsPageRequest, ApiResponse<Page<ProjectDTO>>>
{
    private readonly IProjectRepository _projectRepository;

	public GetProjectsPageHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
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
            .Get(specification)
            .AsPageAsync(request.PageNumber, request.PageSize, cancellationToken);

        var pageDTO = new Page<ProjectDTO>(page.PageNumber, page.PageSize, page.TotalCount, page.Collection.Select(x => x.ToDTO()));

        return ApiResponse.Success(pageDTO);
	}
}
