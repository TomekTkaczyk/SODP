using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

internal class RestoreProjectHandler : IRequestHandler<RestoreProjectRequest>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFolderManager _folderManager;

	public RestoreProjectHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
		IFolderManager folderManager)
    {
		_projectRepository = projectRepository;
		_unitOfWork = unitOfWork;
		_folderManager = folderManager;
	}

	public async Task<Unit> Handle(RestoreProjectRequest request, CancellationToken cancellationToken)
	{
		var project = await _projectRepository
			.Get(new ByIdSpecification<Project>(request.Id))
			.FirstOrDefaultAsync(cancellationToken)
			?? throw new ProjectNotFoundException();

		if(project.Status is not ProjectStatus.Archival)
		{
			throw new BadProjectStatusException();
		}

		project.ChangeStatus(ProjectStatus.DuringRestore);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var (Success, Message) = await _folderManager.RestoreFolderAsync(project, cancellationToken);
		if (!Success)
		{
			project.ChangeStatus(ProjectStatus.Archival);
			throw new FailProjectFolderException(Message);
		}

		project.ChangeStatus(ProjectStatus.Active);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
