using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Stages;

public class DeleteStageCommandHandler : ICommandHandler<DeleteStageCommad>
{
	private readonly IStageRepository _stageRepository;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteStageCommandHandler(
        IStageRepository stageRepository,
        IUnitOfWork unitOfWork)
    {
		_stageRepository = stageRepository;
		_unitOfWork = unitOfWork;
	}

    public async Task<ApiResponse> Handle(DeleteStageCommad request, CancellationToken cancellationToken)
	{
		var stage = await _stageRepository
			.ApplySpecyfication(new StageByIdSpecyfication(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if(stage is null)
		{
			var error = new Error("StageDelete.NotFound",$"Stage Id:{request.Id} not found.",HttpStatusCode.NotFound);
			return ApiResponse.Failure(error,HttpStatusCode.NotFound);
		}

		_stageRepository.Delete(stage);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(HttpStatusCode.NoContent);
	}
}
