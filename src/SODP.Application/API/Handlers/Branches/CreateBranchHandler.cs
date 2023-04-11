using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public sealed class CreateBranchHandler : IRequestHandler<CreateBranchRequest, Branch>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBranchHandler(
        IBranchRepository investorRepository,
        IUnitOfWork unitOfWork)
    {
        _branchRepository = investorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Branch> Handle(CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var branchExist = await _branchRepository
            .ApplySpecyfication(new BranchByNameSpecification(null, request.Name))
            .AnyAsync(cancellationToken);

        if (branchExist)
        {
            throw new ConflictException("Branch");
        }

        var branch = _branchRepository.Add(Branch.Create(request.Sign, request.Name));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return branch;
    }
}
