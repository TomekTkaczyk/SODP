using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Stages;

public class DeleteStageCommandHandler : IRequestHandler<DeleteStageCommad>
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

    public async Task<Unit> Handle(DeleteStageCommad request, CancellationToken cancellationToken)
	{
		var stage = await _stageRepository
			.ApplySpecyfication(new StageByIdSpecyfication(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if(stage is null)
		{
			throw new NotFoundException("Stage");
		}

		_stageRepository.Delete(stage);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
