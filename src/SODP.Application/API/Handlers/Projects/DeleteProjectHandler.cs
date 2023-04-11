using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public sealed class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(
        DeleteProjectRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .ApplySpecyfication(new ProjectByIdSpecification(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        if (project is null)
        {
            throw new NotFoundException("Project");
        }

        _projectRepository.Delete(project);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
