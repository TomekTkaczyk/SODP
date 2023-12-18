using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Parts;
using SODP.Application.Specifications.Common;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Parts;

public sealed class DeletePartHandler : IRequestHandler<DeletePartRequest>
{
	private readonly IPartRepository _partRepository;
	private readonly IUnitOfWork _unitOfWork;

	public DeletePartHandler(
		IPartRepository partRepository,
		IUnitOfWork unitOfWork)
	{
		_partRepository = partRepository;
		_unitOfWork = unitOfWork;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<Unit> Handle(DeletePartRequest request, CancellationToken cancellationToken)
	{
		var part = await _partRepository
			.Get(new ByIdSpecification<Part>(request.Id))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("Part");

		_partRepository.Delete(part);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
