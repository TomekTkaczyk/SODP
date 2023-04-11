using MediatR;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Branches;

public sealed class GetBranchByIdWithLicensesQueryHandler : IRequestHandler<GetBranchByIdWithLicensesQuery, Branch>
{
	private readonly IBranchRepository _branchRepository;

	public GetBranchByIdWithLicensesQueryHandler(
			IBranchRepository branchRepository)
	{
		_branchRepository = branchRepository;
	}

	public async Task<Branch> Handle(
		GetBranchByIdWithLicensesQuery request,
		CancellationToken cancellationToken)
	{
		var branch = await _branchRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);

		return branch ?? throw new NotFoundException("Branch");
	}
}
