using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Common;
using SODP.Application.Specifications.Projects;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		var specification = new ByIdSpecification<PartBranch>(request.PartBranchId);
		var partBranch = await _partBranchRepository.Get(specification)
			.Include(b => b.Branch)
			.Include(x => x.Roles)
			.ThenInclude(x => x.License)
			.ThenInclude(x => x.Designer)
			.SingleOrDefaultAsync(cancellationToken);

		var dto = _mapper.Map<PartBranchDTO>(partBranch);

		return ApiResponse.Success(_mapper.Map<PartBranchDTO>(partBranch));
	}
}
