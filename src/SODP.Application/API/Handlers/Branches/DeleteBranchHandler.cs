﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public sealed class DeleteBranchHandler : IRequestHandler<DeleteBranchRequest>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBranchHandler(
        IBranchRepository branchRepository,
        IUnitOfWork unitOfWork)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteBranchRequest request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository
            .ApplySpecyfication(new BranchByIdSpecification(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        if (branch is null)
        {
            throw new NotFoundException("Branch");
        }

        _branchRepository.Delete(branch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}