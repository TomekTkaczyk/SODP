using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Common;
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
	private readonly IUnitOfWork _unitOfWork;

	public AddRoleToPartBranchHandler(
		IPartBranchRepository partBranchRepository,
		ILicensesRepository licensesRepository,
        IUnitOfWork unitOfWork)
    {
		_partBranchRepository = partBranchRepository;
		_licensesRepository = licensesRepository;
		_unitOfWork = unitOfWork;
	}
    public async Task<Unit> Handle(AddRoleToPartBranchRequest request, CancellationToken cancellationToken)
	{
		var partBranch = await _partBranchRepository.Get(new ByIdSpecification<PartBranch>(request.PartBranchId))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new PartBranchNotFoundException();

		var license = await _licensesRepository.Get(new ByIdSpecification<License>(request.LicenseId))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new LicenseNotFoundException();

		partBranch.AddRole(request.Role, license);

		_partBranchRepository.Update(partBranch);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
