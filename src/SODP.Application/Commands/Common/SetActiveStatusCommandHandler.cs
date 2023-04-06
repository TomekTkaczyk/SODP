using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Common;

public sealed class SetActiveStatusCommandHandler<TEntity>
	: IRequestHandler<SetActiveStatusCommand<TEntity>, ApiResponse> where TEntity : BaseEntity, IActiveStatus
{
	private readonly IRepository<TEntity> _repository;
	private readonly IUnitOfWork _unitOfWork;

	public SetActiveStatusCommandHandler(
		IRepository<TEntity> repository,
		IUnitOfWork unitOfWork)
    {
		_repository = repository;
		_unitOfWork = unitOfWork;
	}
    public async Task<ApiResponse> Handle(SetActiveStatusCommand<TEntity> request, CancellationToken cancellationToken)
	{
		var entity = await _repository
			.ApplySpecyfication(new ByIdSpecification<TEntity>(request.Id))
			.SingleOrDefaultAsync(cancellationToken);
		if (entity is null)
		{
			var error = new Error($"SetActive.{typeof(TEntity)}", $"{typeof(TEntity)} not found.", HttpStatusCode.NotFound);
			return ApiResponse.Failure(error, HttpStatusCode.NotFound);
		}
		entity.SetActiveStatus(request.ActiveStatus);
		_repository.Update(entity);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(HttpStatusCode.NoContent);
	}
}

















	//public async Task<ApiResponse> Handle(SetActiveStatusCommand<TEntity> request, CancellationToken cancellationToken)
	//{
	//	var entity = await _repository
	//		.ApplySpecyfication(new ByIdSpecification<TEntity>(request.Id))
	//		.SingleOrDefaultAsync(cancellationToken);
	//	if (entity is null)
	//	{
	//		var error = new Error($"SetActive.{typeof(TEntity)}", $"{typeof(TEntity)} not found.", HttpStatusCode.NotFound);
	//		return ApiResponse.Failure(error, HttpStatusCode.NotFound);
	//	}
	//	entity.SetActiveStatus(request.ActiveStatus);
	//	_repository.Update(entity);
	//	await _unitOfWork.SaveChangesAsync(cancellationToken);

	//	return ApiResponse.Success(HttpStatusCode.NoContent);
	//}
//}
