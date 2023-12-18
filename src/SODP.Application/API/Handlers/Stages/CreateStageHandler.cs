using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Stages;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.StageExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Stages;

public sealed class CreateStageHandler : IRequestHandler<CreateStageRequest, ApiResponse<int>>
{
    private readonly IStageRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

	public CreateStageHandler(
        IStageRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<int>> Handle(CreateStageRequest request, CancellationToken cancellationToken)
    {
        var specification = new StageBySignSpecification(request.Sign.ToUpper());

		var stageExist = await _repository
            .Get(specification)
            .AnyAsync(cancellationToken);

        if (stageExist)
        {
            throw new StageConflictException();
        }

        var stage = _repository.Add(Stage.Create(request.Sign, request.Title));
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(stage.Id);
    }
}
