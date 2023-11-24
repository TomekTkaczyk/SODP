using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Parts;
using SODP.Application.Specifications.Branches;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Parts;

public sealed class UpdatePartHandler : IRequestHandler<UpdatePartRequest>
{
	private readonly IPartRepository _partRepository;
	private readonly IUnitOfWork _unitOfWork;

	public UpdatePartHandler(
		IPartRepository partRepository,
		IUnitOfWork unitOfWork)
	{
		_partRepository = partRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdatePartRequest request, CancellationToken cancellationToken)
	{
		var part = await _partRepository
			.Get(new ByIdSpecification<Part>(request.Id))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new NotFoundException("Part");

		part.SetSign(request.Sign);
		part.SetTitle(request.Title);

		_partRepository.Update(part);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
