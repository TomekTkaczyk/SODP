using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

internal class SetActiveStatusBranchHandler : IRequestHandler<SetActiveStatusBranchRequest>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetActiveStatusBranchHandler(
        IBranchRepository branchRepository,
        IUnitOfWork unitOfWork)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(SetActiveStatusBranchRequest request, CancellationToken cancellationToken)
    {
        var investor = await _branchRepository
            .ApplySpecyfication(new BranchByIdSpecification(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        if (investor is null)
        {
            throw new NotFoundException("Branch");
        }

        investor.SetActiveStatus(request.ActiveStatus);
        _branchRepository.Update(investor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
