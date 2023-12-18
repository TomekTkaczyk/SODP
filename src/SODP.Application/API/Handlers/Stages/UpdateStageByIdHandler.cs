using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Stages;
using SODP.Application.Specifications.Common;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.StageExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Stages;

internal sealed class UpdateStageByIdHandler : IRequestHandler<UpdateStageByIdRequest, ApiResponse>
{
    private readonly IStageRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStageByIdHandler(
        IStageRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse> Handle(UpdateStageByIdRequest request, CancellationToken cancellationToken)
    {
		var stage = await _repository
            .Get(new ByIdSpecification<Stage>(request.Id))
            .SingleOrDefaultAsync(cancellationToken) 
            ?? throw new StageNotFoundException();

		stage.SetTitle(request.Title);

        _repository.Update(stage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success();
    }
}
