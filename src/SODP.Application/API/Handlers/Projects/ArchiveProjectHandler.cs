using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Common;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

internal class ArchiveProjectHandler : IRequestHandler<ArchiveProjectRequest>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IFolderManager _folderManager;

	public ArchiveProjectHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
		IFolderManager folderManager)
    {
		_projectRepository = projectRepository;
		_unitOfWork = unitOfWork;
		_folderManager = folderManager;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<Unit> Handle(ArchiveProjectRequest request, CancellationToken cancellationToken)
	{
		var project = await _projectRepository
			.Get(new ByIdSpecification<Project>(request.Id))
			.FirstOrDefaultAsync(cancellationToken)
			?? throw new ProjectNotFoundException();

		if(project.Status is not ProjectStatus.Active)
		{
			throw new BadProjectStatusException();
		}

		project.ChangeStatus(ProjectStatus.DuringArchive);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var (Success, Message) = await _folderManager.ArchiveProjectFolderAsync(project, cancellationToken);
		if (!Success)
		{
			project.ChangeStatus(ProjectStatus.Active);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			throw new FailProjectFolderException(Message);
		}

		project.ChangeStatus(ProjectStatus.Archival);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
