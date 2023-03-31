using SODP.Application.Abstractions;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Stages;

internal sealed class ChangeStageNameCommandHandler : ICommandHandler<ChangeStageNameCommand>
{
	private readonly IStageRepository _stageRepository;
	private readonly IUnitOfWork _unitOfWork;

	public ChangeStageNameCommandHandler(IStageRepository stageRepository, IUnitOfWork unitOfWork)
    {
		_stageRepository = stageRepository;
		_unitOfWork = unitOfWork;
	}
    public async Task<ApiResponse> Handle(ChangeStageNameCommand request, CancellationToken cancellationToken)
	{
		var stage = await _stageRepository.GetByIdAsync(request.Id, cancellationToken);
		if(stage is null)
		{
			return ApiResponse.Failure(new Error("UpdateStage",$"Stage Id:{request.Id} not exist."));
		}

		stage.Name = request.Name;
		_stageRepository.Update(stage);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success();
	}
}
