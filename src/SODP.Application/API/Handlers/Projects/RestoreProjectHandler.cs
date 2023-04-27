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
			.ApplySpecyfication(new ByIdSpecification<Project>(request.Id))
			.FirstOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("Project not found.");

		if(project.Status is not ProjectStatus.Archival)
		{
			throw new BadRequestException("Project is not archival.");
		}

		project.Status = ProjectStatus.DuringRestore;
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var (Success, Message) = await _folderManager.RestoreFolderAsync(project, cancellationToken);
		if (!Success)
		{
            throw new ProjectFolderException($"Restore folder fail: {Message}");
		}

		project.Status = ProjectStatus.Active;
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
