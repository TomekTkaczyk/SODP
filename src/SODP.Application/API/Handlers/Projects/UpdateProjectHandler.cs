using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Exceptions;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

internal sealed class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IFolderManager _folderManager;
	private readonly IUnitOfWork _unitOfWork;

	public UpdateProjectHandler(
        IProjectRepository projectRepository,
        IFolderManager folderManager,
		IUnitOfWork unitOfWork)
    {
		_projectRepository = projectRepository;
		_folderManager = folderManager;
		_unitOfWork = unitOfWork;
	}

    public async Task<Unit> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.Get(new ProjectByIdSpecification(request.Id))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ProjectNotFoundException();

		if(!project.Status.Equals(ProjectStatus.Active))
		{
			throw new BadProjectStatusException();
		}

		project.Update(
			request.Name,
			request.Title,
			request.Address,
			request.LocationUnit,
			request.BuildingCategory,
			request.Investor,
			request.BuildingPermit,
			request.Description,
			request.DevelopmentDate
		);

		var (Success, Message) = await _folderManager.MatchProjectFolderAsync(project, cancellationToken);
		if (!Success)
		{
			throw new ProjectFolderException($"Rename folder fail: {Message}");
		}

		_projectRepository.Update(project);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
