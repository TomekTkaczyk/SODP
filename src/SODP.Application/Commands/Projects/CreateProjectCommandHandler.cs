using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Projects;

internal class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, Project>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IStageRepository _stageRepository;
	private readonly IUnitOfWork _unitOfWork;

	public CreateProjectCommandHandler(
		IProjectRepository projectRepository, 
		IStageRepository stageRepository,
		IUnitOfWork unitOfWork)
    {
		_projectRepository = projectRepository;
		_stageRepository = stageRepository;
		_unitOfWork = unitOfWork;
	}

    public async Task<ApiResponse<Project>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
	{
		var projectExist = await _projectRepository
			.ApplySpecyfication(new ProjectBySymbolSpecyfication(request.Number, request.StageSign))
			.AnyAsync(cancellationToken);

		if(projectExist)
		{
			var error = new Error("ProjectCreator", "Project already exist.", HttpStatusCode.Conflict);
			return ApiResponse.Failure<Project>(error, HttpStatusCode.Conflict);
		}

		var stage = await _stageRepository
			.ApplySpecyfication(new StageBySignSpecyfication(request.StageSign))
			.SingleOrDefaultAsync(cancellationToken);

		if(stage is null)
		{
			var error = new Error("ProjectCreator", "Stage not found.", HttpStatusCode.NotFound);
			return ApiResponse.Failure<Project>(error, HttpStatusCode.NotFound);
		}


		var project = Project.Create(request.Number, request.StageSign, request.Name);
		project.Description = request.Description;
		project = _projectRepository.Add(project);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(project, HttpStatusCode.Created);
	}
}
