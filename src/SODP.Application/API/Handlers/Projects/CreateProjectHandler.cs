using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

internal class CreateProjectHandler : IRequestHandler<CreateProjectRequest, ApiResponse<ProjectDTO>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IStageRepository _stageRepository;
	private readonly IFolderManager _folderManager;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateProjectHandler(
        IProjectRepository projectRepository,
        IStageRepository stageRepository,
        IFolderManager folderManager,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _stageRepository = stageRepository;
		_folderManager = folderManager;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

    public async Task<ApiResponse<ProjectDTO>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var projectExist = await _projectRepository
            .ApplySpecyfication(new ProjectBySymbolSpecyfication(request.Number, request.StageSign))
            .AnyAsync(cancellationToken);

        if (projectExist)
        {
            throw new ConflictException("Project");
        }

        var stage = await _stageRepository
            .ApplySpecyfication(new StageBySignSpecyfication(request.StageSign))
            .SingleOrDefaultAsync(cancellationToken);

        if (stage is null)
        {
            throw new NotFoundException("Project:Stage");
        }

        var project = Project.Create(request.Number, stage, request.Name);
        
        project.Description = request.Description;

        var (Success,Message) = await _folderManager.CreateFolderAsync(project, cancellationToken);
        if(!Success)
        {
            throw new ProjectFolderException($"Create folder fail: {Message}");
        }

        project = _projectRepository.Add(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(_mapper.Map<ProjectDTO>(project));
    }
}
