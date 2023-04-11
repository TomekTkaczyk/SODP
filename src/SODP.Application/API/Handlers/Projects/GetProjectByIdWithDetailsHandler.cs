using MediatR;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetProjectByIdWithDetailsHandler : IRequestHandler<GetProjectByIdWithDetailsRequest, Project>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdWithDetailsHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Project> Handle(
        GetProjectByIdWithDetailsRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetByIdWithDetailsAsync(request.Id, cancellationToken);

        return project ?? throw new NotFoundException("Project");
    }
}
