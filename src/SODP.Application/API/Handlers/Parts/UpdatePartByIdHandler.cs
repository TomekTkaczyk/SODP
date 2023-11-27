using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Parts;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.PartExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Parts;

public sealed class UpdatePartByIdHandler : IRequestHandler<UpdatePartByIdRequest, ApiResponse>
{
	private readonly IPartRepository _repository;
	private readonly IUnitOfWork _unitOfWork;

	public UpdatePartByIdHandler(
		IPartRepository repository,
		IUnitOfWork unitOfWork)
	{
		_repository = repository;
		_unitOfWork = unitOfWork;
	}

	public async Task<ApiResponse> Handle(UpdatePartByIdRequest request, CancellationToken cancellationToken)
	{
		var part = await _repository
			.Get(new ByIdSpecification<Part>(request.Id))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new PartNotFoundException();

		part.SetTitle(request.Title);

		_repository.Update(part);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success();
	}
}
