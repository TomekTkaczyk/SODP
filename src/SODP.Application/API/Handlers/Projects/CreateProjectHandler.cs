﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

internal class CreateProjectHandler : IRequestHandler<CreateProjectRequest, Project>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IStageRepository _stageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectHandler(
        IProjectRepository projectRepository,
        IStageRepository stageRepository,
        IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _stageRepository = stageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Project> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
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

        var project = Project.Create(request.Number, request.StageSign, request.Name);
        project.Description = request.Description;
        project = _projectRepository.Add(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return project;
    }
}