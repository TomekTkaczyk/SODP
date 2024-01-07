using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Licenses;
using SODP.Application.Specifications.Common;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.LicenseExceptions;
using SODP.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public sealed class ChangeLicenseContentHandler : IRequestHandler<ChangeLicenseContentRequest>
{
	private readonly ILicensesRepository _licensesRepository;
	private readonly IUnitOfWork _unitOfWork;

	public ChangeLicenseContentHandler(
		ILicensesRepository licensesRepository,
		IUnitOfWork unitOfWork)
    {
		_licensesRepository = licensesRepository;
		_unitOfWork = unitOfWork;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<Unit> Handle(ChangeLicenseContentRequest request, CancellationToken cancellationToken)
	{
		var license = await _licensesRepository.Get(new ByIdSpecification<License>(request.Id))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new LicenseNotFoundException();

		if( _licensesRepository.Get()
			.Where(x => x.Id != request.Id && x.Content.Equals(request.Content))
			.Any()) 
		{
			throw new LicenseContentExistException();
		}

		license.Content = request.Content;

		_licensesRepository.Update(license);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
