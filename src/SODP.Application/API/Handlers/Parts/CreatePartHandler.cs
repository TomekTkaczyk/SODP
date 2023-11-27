using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Parts;
using SODP.Application.Specifications.Parts;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.PartExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Parts;

public sealed class CreatePartHandler : IRequestHandler<CreatePartRequest, ApiResponse<int>>
{
	private readonly IPartRepository _repository;
	private readonly IUnitOfWork _unitOfWork;

	public CreatePartHandler(
        IPartRepository repository,
		IUnitOfWork unitOfWork)
    {
		_repository = repository;
		_unitOfWork = unitOfWork;
	}

    public async Task<ApiResponse<int>> Handle(CreatePartRequest request, CancellationToken cancellationToken)
	{
		var specification = new PartBySignSpecification(request.Sign.ToUpper());

		var partExist = await _repository
			.Get(specification)
			.AnyAsync(cancellationToken);

		if(partExist)
		{
			throw new PartConflictException();
		}

		var part = _repository.Add(Part.Create(request.Sign, request.Title));

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(part.Id);
	}
}
