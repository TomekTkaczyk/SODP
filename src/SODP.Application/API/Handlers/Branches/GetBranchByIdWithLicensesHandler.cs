using MediatR;
using SODP.Application.API.Requests.Branches;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public sealed class GetBranchByIdWithLicensesHandler : IRequestHandler<GetBranchByIdWithLicensesRequest, Branch>
{
    private readonly IBranchRepository _branchRepository;

    public GetBranchByIdWithLicensesHandler(
            IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Branch> Handle(
        GetBranchByIdWithLicensesRequest request,
        CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);

        return branch ?? throw new NotFoundException("Branch");
    }
}
