using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Common;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetProjectHandler : IRequestHandler<GetProjectRequest, ApiResponse<ProjectDTO>>
{
    private readonly IProjectRepository _projectRepository;
	private readonly IMapper _mapper;

	public GetProjectHandler(
        IProjectRepository projectRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<ProjectDTO>> Handle(
        GetProjectRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .Get(new ByIdSpecification<Project>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException("Project");

		return ApiResponse.Success(_mapper.Map<ProjectDTO>(project));
    }
}
