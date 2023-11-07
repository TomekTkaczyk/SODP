using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Stages;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Stages;

internal sealed class ChangeStageTitleHandler : IRequestHandler<ChangeStageTitleRequest, ApiResponse>
{
    private readonly IStageRepository _stageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeStageTitleHandler(
        IStageRepository stageRepository,
        IUnitOfWork unitOfWork)
    {
        _stageRepository = stageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(ChangeStageTitleRequest request, CancellationToken cancellationToken)
    {
        var stage = await _stageRepository
            .ApplySpecyfication(new StageByIdSpecyfication(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        if (stage is null)
        {
            throw new NotFoundException("Stage");
        }

        stage.SetTitle(request.Title);

        _stageRepository.Update(stage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success();
    }
}
