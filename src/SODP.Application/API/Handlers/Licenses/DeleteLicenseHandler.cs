using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Licenses;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public sealed class DeleteLicenseHandler : IRequestHandler<DeleteLicenseRequest>
{
	private readonly IProjectRepository _projectRepository;
	private readonly ILicensesRepository _licensesRepository;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteLicenseHandler(
		IProjectRepository projectRepository,
		ILicensesRepository licensesRepository,
		IUnitOfWork unitOfWork)
    {
		_projectRepository = projectRepository;
		_licensesRepository = licensesRepository;
		_unitOfWork = unitOfWork;
	}

    public async Task<Unit> Handle(
		DeleteLicenseRequest request, 
		CancellationToken cancellationToken)
	{
		var license = await _licensesRepository
			.Get(new ByIdSpecification<License>(request.Id))
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
			?? throw new NotFoundException(nameof(License));

		_licensesRepository.Delete(license);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
