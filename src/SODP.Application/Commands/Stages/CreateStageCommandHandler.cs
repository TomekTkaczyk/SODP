using SODP.Application.Abstractions;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Stages;

public sealed class CreateStageCommandHandler : ICommandHandler<CreateStageCommand, Stage>
{
	private readonly IStageRepository _stageRepository;
	private readonly IUnitOfWork _unitOfWork;

	public CreateStageCommandHandler(
		IStageRepository stageRepository, 
		IUnitOfWork unitOfWork)
    {
		_stageRepository = stageRepository;
		_unitOfWork = unitOfWork;
	}

    public async Task<ApiResponse<Stage>> Handle(CreateStageCommand request, CancellationToken cancellationToken)
	{
		var stage = await _stageRepository.GetBySignAsync(request.Sign, cancellationToken);

		if (stage is not null)
		{
			var error = new Error("StageCreator", "Stage already exist.", HttpStatusCode.Conflict);
			return ApiResponse.Failure<Stage>(error, HttpStatusCode.Conflict);
		}

		stage = _stageRepository.Add(Stage.Create(request.Sign, request.Name));
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(stage);
	}
}
