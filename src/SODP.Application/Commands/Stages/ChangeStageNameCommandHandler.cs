using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Investors;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Stages;

internal sealed class ChangeStageNameCommandHandler : ICommandHandler<ChangeStageNameCommand>
{
	private readonly IStageRepository _stageRepository;
	private readonly IUnitOfWork _unitOfWork;

	public ChangeStageNameCommandHandler(
		IStageRepository stageRepository, 
		IUnitOfWork unitOfWork)
    {
		_stageRepository = stageRepository;
		_unitOfWork = unitOfWork;
	}

    public async Task<ApiResponse> Handle(ChangeStageNameCommand request, CancellationToken cancellationToken)
	{
		var stage = await _stageRepository
			.ApplySpecyfication(new StageByNameAndDifferentIdSpecification(request.Id, request.Name))
			.FirstOrDefaultAsync(cancellationToken);

		if(stage is not null)
		{
			var error = new Error("ChangeStageName", $"Different stage has the same name.", HttpStatusCode.NotFound);
			return ApiResponse.Failure(error, HttpStatusCode.NotFound);
		}

		stage = await _stageRepository
			.ApplySpecyfication(new StageByIdSpecyfication(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if(stage is null)
		{
			var error = new Error("CahngeStageName", $"Stage id:{request.Id} not found.", HttpStatusCode.NotFound);
			return ApiResponse.Failure(error, HttpStatusCode.NotFound);
		}

		stage.Name = request.Name;
		_stageRepository.Update(stage);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success();
	}
}
