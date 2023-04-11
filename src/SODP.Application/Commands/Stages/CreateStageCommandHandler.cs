using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Stages;

public sealed class CreateStageCommandHandler : IRequestHandler<CreateStageCommand, Stage>
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

    public async Task<Stage> Handle(CreateStageCommand request, CancellationToken cancellationToken)
	{
		var stage = await _stageRepository
			.ApplySpecyfication(new StageBySignSpecyfication(request.Sign.ToUpper()))
			.SingleOrDefaultAsync(cancellationToken);

		if (stage is not null)
		{
			throw new ConflictException("Stage");
		}

		stage = _stageRepository.Add(Stage.Create(request.Sign.ToUpper(), request.Name.ToUpper()));
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return stage;
	}
}
