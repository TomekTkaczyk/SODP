using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Licenses;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.BranchExceptions;
using SODP.Domain.Exceptions.LicenseExceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public sealed class AddBranchToLicenseHandler : IRequestHandler<AddBranchToLicenseRequest>
{
	private readonly ILicensesRepository _licensesRepository;
	private readonly IBranchRepository _branchRepository;
	private readonly IUnitOfWork _unitOfWork;

	public AddBranchToLicenseHandler(
        ILicensesRepository licensesRepository,
		IBranchRepository branchRepository,
        IUnitOfWork unitOfWork)
    {
		_licensesRepository = licensesRepository;
		_branchRepository = branchRepository;
		_unitOfWork = unitOfWork;
	}

    public async Task<Unit> Handle(AddBranchToLicenseRequest request, CancellationToken cancellationToken)
	{
		var licence = await _licensesRepository.Get(new ByIdSpecification<License>(request.Id))
			.Include(x => x.Designer)
			.Include(x => x.Branches)
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new LicenseNotFoundException();

		var branch = await _branchRepository.Get(new ByIdSpecification<Branch>(request.BranchId))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new BranchNotFoundException();

		licence.AddBranch(branch);

		_licensesRepository.Update(licence);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
