using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Stages;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Stages;

public class GetStageByIdQueryHandler : IRequestHandler<GetStageByIdQuery, Stage>
{
	private readonly IStageRepository _stageRepository;

	public GetStageByIdQueryHandler(
		IStageRepository stageRepository)
    {
		_stageRepository = stageRepository;
	}

    public async Task<Stage> Handle(
		GetStageByIdQuery request, 
		CancellationToken cancellationToken)
	{
		var stage = await _stageRepository
			.ApplySpecyfication(new StageByIdSpecyfication(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		return stage ?? throw new NotFoundException("Stage");
	}
}
