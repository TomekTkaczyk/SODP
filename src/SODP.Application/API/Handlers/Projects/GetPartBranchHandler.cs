using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Mappers;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.PartBranchExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public sealed class GetPartBranchHandler : IRequestHandler<GetPartBranchRequest, ApiResponse<PartBranchDTO>>
{
	private readonly IPartBranchRepository _partBranchRepository;
	private readonly IMapper _mapper;

	public GetPartBranchHandler(
        IPartBranchRepository partBranchRepository,
        IMapper mapper)
    {
		_partBranchRepository = partBranchRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<PartBranchDTO>> Handle(GetPartBranchRequest request, CancellationToken cancellationToken)
	{
		var mapCache = new List<object>();

		var specification = new ByIdSpecification<PartBranch>(request.PartBranchId);
		var partBranch = await _partBranchRepository.Get(specification)
			.Include(b => b.Branch)
			.Include(x => x.Roles)
			.ThenInclude(x => x.License)
			.ThenInclude(x => x.Designer)
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new PartBranchNotFoundException();


		var partBranchDto = partBranch.ToDTO(mapCache);

		var result = ApiResponse.Success(partBranchDto);

		return result;
	}
}
