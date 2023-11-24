using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Stages;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Exceptions.StageExceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Stages;

public class DeleteStageHandler : IRequestHandler<DeleteStageRequest>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IStageRepository _stageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStageHandler(
        IProjectRepository projectRepository,
        IStageRepository stageRepository,
        IUnitOfWork unitOfWork)
    {
		_projectRepository = projectRepository;
		_stageRepository = stageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteStageRequest request, CancellationToken cancellationToken)
    {
		var stage = await _stageRepository
			.Get(new StageByIdSpecyfication(request.Id))
			.SingleOrDefaultAsync(cancellationToken) 
            ?? throw new StageNotFoundException();

		var projectUseSign = await _projectRepository
            .Get(new ProjectBySymbolSpecyfication(null, stage.Sign))
            .AnyAsync(cancellationToken);
		
        if (projectUseSign)
        {
            throw new StageIsInUseException();
		}

        _stageRepository.Delete(stage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
