using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Licenses;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.LicenseExceptions;
using SODP.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses
{
	internal class RemoveBranchFromLicenseHandler : IRequestHandler<RemoveBranchFromLicenseRequest>
	{
		private readonly ILicensesRepository _licensesRepository;
		private readonly IUnitOfWork _unitOfWork;

		public RemoveBranchFromLicenseHandler(
			ILicensesRepository licensesRepository,
			IUnitOfWork unitOfWork)
		{
			_licensesRepository = licensesRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Unit> Handle(RemoveBranchFromLicenseRequest request, CancellationToken cancellationToken)
		{
			var license = await _licensesRepository.Get(new ByIdSpecification<License>(request.Id))
				.Include(x => x.Branches)
				.SingleOrDefaultAsync(cancellationToken)
				?? throw new LicenseNotFoundException();

			if (!license.DeleteBranch(request.BranchId))
			{
				throw new Exception("[RemoveBranchFromLicense] : Somthing went wrong");
			}

			_licensesRepository.Update(license);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return new Unit();
		}
	}
}
