using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
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
	private readonly IFolderManager _folderManager;

	public DeleteProjectHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
        IFolderManager folderManager)
    {
        _unitOfWork = unitOfWork;
		_folderManager = folderManager;
		_projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(
        DeleteProjectRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .Get(new ProjectByIdSpecification(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException("Project");

        var (Success, Message) = await _folderManager.DeleteProjectFolderAsync(project, cancellationToken);
        if(!Success)
        {
            throw new ProjectFolderException($"Delete project fail: {Message}");
        }

        _projectRepository.Delete(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
