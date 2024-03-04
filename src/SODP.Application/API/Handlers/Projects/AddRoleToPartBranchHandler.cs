using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Common;
using SODP.Application.Specifications.Projects;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.LicenseExceptions;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class AddRoleToPartBranchHandler : IRequestHandler<AddRoleToPartBranchRequest>
{
	private readonly IPartBranchRepository _partBranchRepository;
	private readonly ILicensesRepository _licensesRepository;
	private readonly IBranchLicenseRepository _branchLicenseRepository;
	private readonly IUnitOfWork _unitOfWork;

	public AddRoleToPartBranchHandler(
		IPartBranchRepository partBranchRepository,
		ILicensesRepository licensesRepository,
		IBranchLicenseRepository branchLicenseRepository,
        IUnitOfWork unitOfWork)
    {
		_partBranchRepository = partBranchRepository;
		_licensesRepository = licensesRepository;
		_branchLicenseRepository = branchLicenseRepository;
		_unitOfWork = unitOfWork;
	}
    public async Task<Unit> Handle(AddRoleToPartBranchRequest request, CancellationToken cancellationToken)
	{
		var partBranch = await _partBranchRepository.Get(new PartBranchWithDetailsSpecification(request.PartBranchId))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new PartBranchNotFoundException();

		var branchLicense = await _branchLicenseRepository.Get(new ByIdSpecification<BranchLicense>(request.LicenseId))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new LicenseNotFoundException();

		var license = await _licensesRepository.Get(new ByIdSpecification<License>(branchLicense.LicenseId))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new LicenseNotFoundException();

		partBranch.AddRole(request.Role, license);

		_partBranchRepository.Update(partBranch);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
