﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Projects;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public sealed class DeletePartHandler : IRequestHandler<DeletePartRequest>
{
    private readonly IProjectPartRepository _projectPartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePartHandler(
        IProjectPartRepository projectPartRepository,
        IUnitOfWork unitOfWork)
    {
        _projectPartRepository = projectPartRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Unit> Handle(DeletePartRequest request, CancellationToken cancellationToken)
    {
        var projectPart = await _projectPartRepository
            .Get(new ProjectPartByIdSpecification(request.ProjectPartId))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ProjectPartNotFoundException();


        _projectPartRepository.Delete(projectPart);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}