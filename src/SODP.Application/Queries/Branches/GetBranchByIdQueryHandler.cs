using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Branches;

public sealed class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, Branch>
{
	private readonly IBranchRepository _branchRepository;

	public GetBranchByIdQueryHandler(
		IBranchRepository branchRepository)
    {
		_branchRepository = branchRepository;
	}
    public async Task<Branch> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
	{
		var branch = await _branchRepository
			.ApplySpecyfication(new BranchByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		return branch ?? throw new NotFoundException("Branch");
	}
}
