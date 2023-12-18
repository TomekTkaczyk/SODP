using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Exceptions.StageExceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.Response;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

internal class CreateProjectHandler : IRequestHandler<CreateProjectRequest, ApiResponse<int>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IStageRepository _stageRepository;
	private readonly IFolderManager _folderManager;
	private readonly IUnitOfWork _unitOfWork;

	public CreateProjectHandler(
        IProjectRepository projectRepository,
        IStageRepository stageRepository,
        IFolderManager folderManager,
        IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _stageRepository = stageRepository;
		_folderManager = folderManager;
		_unitOfWork = unitOfWork;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<int>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
		var stage = await _stageRepository
			.Get(new StageBySignSpecification(request.StageSign))
			.SingleOrDefaultAsync(cancellationToken) 
            ?? throw new StageNotFoundException();

		var project = Project.Create(request.Number, stage, request.Name);

        var projectExist = await _projectRepository
            .Get(new ProjectBySymbolSpecification(request.Number, request.StageSign))
            .AnyAsync(cancellationToken);

        if (projectExist)
        {
            throw new ConflictException("Project");
        }

		var (Success, Message) = await _folderManager.CreateProjectFolderAsync(project, cancellationToken);
		if (!Success)
		{
			throw new ProjectFolderException($"Create folder fail: {Message}");
		}

		project = _projectRepository.Add(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(project.Id);
    }
}
