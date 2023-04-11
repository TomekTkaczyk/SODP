using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Stages;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdRequest, Project>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Project> Handle(
        GetProjectByIdRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .ApplySpecyfication(new ProjectByIdSpecification(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        return project ?? throw new NotFoundException("Project");
    }
}
