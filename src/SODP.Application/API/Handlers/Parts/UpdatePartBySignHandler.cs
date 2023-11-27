using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Parts;
using SODP.Application.Specifications.Common;
using SODP.Application.Specifications.Parts;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.PartExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Parts;

internal sealed class UpdatePartBySignHandler : IRequestHandler<UpdatePartBySignRequest, ApiResponse>
{
	private readonly IPartRepository _repository;
	private readonly IUnitOfWork _unitOfWork;

	public UpdatePartBySignHandler(
		IPartRepository repository,
		IUnitOfWork unitOfWork)
	{
		_repository = repository;
		_unitOfWork = unitOfWork;
	}

	public async Task<ApiResponse> Handle(UpdatePartBySignRequest request, CancellationToken cancellationToken)
	{
		var part = await _repository
			.Get(new PartBySignSpecification(request.Sign))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new PartNotFoundException();

		part.SetTitle(request.Title);

		_repository.Update(part);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success();
	}
}
