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

public sealed class GetBranchByIdHandler : IRequestHandler<GetBranchByIdRequest, Branch>
{
    private readonly IBranchRepository _branchRepository;

    public GetBranchByIdHandler(
        IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }
    public async Task<Branch> Handle(GetBranchByIdRequest request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository
            .ApplySpecyfication(new BranchByIdSpecification(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        return branch ?? throw new NotFoundException("Branch");
    }
}
