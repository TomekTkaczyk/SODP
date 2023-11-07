using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public class UpdateBranchHandler : IRequestHandler<UpdateBranchRequest>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBranchHandler(
        IBranchRepository branchRepository,
        IUnitOfWork unitOfWork)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateBranchRequest request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository
            .ApplySpecyfication(new ByIdSpecification<Branch>(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

		if (branch is null)
        {
            throw new NotFoundException("Branch");
        }
        
        branch.SetSign(request.Sign);
        branch.SetTitle(request.Title);

        _branchRepository.Update(branch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
