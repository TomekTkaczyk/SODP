using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Stages;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Stages;

internal sealed class ChangeStageNameHandler : IRequestHandler<ChangeStageNameRequest>
{
    private readonly IStageRepository _stageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeStageNameHandler(
        IStageRepository stageRepository,
        IUnitOfWork unitOfWork)
    {
        _stageRepository = stageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ChangeStageNameRequest request, CancellationToken cancellationToken)
    {
        var stage = await _stageRepository
            .ApplySpecyfication(new StageByIdSpecyfication(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        if (stage is null)
        {
            throw new NotFoundException("Stage");
        }

        stage.Title = request.Name.ToUpper();
        _stageRepository.Update(stage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
