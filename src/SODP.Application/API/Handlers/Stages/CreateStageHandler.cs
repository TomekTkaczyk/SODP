using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Stages;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Exceptions.StageExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Stages;

public sealed class CreateStageHandler : IRequestHandler<CreateStageRequest, ApiResponse<StageDTO>>
{
    private readonly IStageRepository _stageRepository;
    private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateStageHandler(
        IStageRepository stageRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _stageRepository = stageRepository;
        _unitOfWork = unitOfWork;
		_mapper = mapper;
	}

    public async Task<ApiResponse<StageDTO>> Handle(CreateStageRequest request, CancellationToken cancellationToken)
    {
        var stageExist = await _stageRepository
            .Get(new StageBySignSpecyfication(request.Sign.ToUpper()))
            .AnyAsync(cancellationToken);

        if (stageExist)
        {
            throw new StageConflictException();
        }

        var stage = _stageRepository.Add(Stage.Create(request.Sign.ToUpper(), request.Title.ToUpper()));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(_mapper.Map<StageDTO>(stage));
    }
}
