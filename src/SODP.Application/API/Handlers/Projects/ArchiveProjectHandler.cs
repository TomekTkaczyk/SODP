using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
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

	public async Task<Unit> Handle(ArchiveProjectRequest request, CancellationToken cancellationToken)
	{
		var project = await _projectRepository
			.ApplySpecyfication(new ByIdSpecification<Project>(request.Id))
			.FirstOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("Project not found.");

		if(project.Status is not ProjectStatus.Active)
		{
			throw new BadRequestException("Project is not active.");
		}

		project.Status = ProjectStatus.DuringArchive;
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var (Success, Message) = await _folderManager.ArchiveFolderAsync(project, cancellationToken);
		if (!Success)
		{
            throw new ProjectFolderException($"Archive folder fail: {Message}");
		}

		project.Status = ProjectStatus.Archival;
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
