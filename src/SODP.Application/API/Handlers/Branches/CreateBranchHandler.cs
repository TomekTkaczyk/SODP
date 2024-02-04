using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.BranchExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public sealed class CreateBranchHandler : IRequestHandler<CreateBranchRequest, ApiResponse<int>>
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

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<int>> Handle(CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var specification = new BranchBySignSpecification(request.Sign);
        var branchExist = await _branchRepository
            .Get(specification)
            .AnyAsync(cancellationToken);

        if (branchExist)
        {
            throw new BranchConflictException();
        }

        var branch = _branchRepository.Add(Branch.Create(request.Sign, request.Title));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(branch.Id);
    }
}
