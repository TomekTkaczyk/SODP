using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Attributes;
using SODP.Domain.Exceptions.PartExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetPartWithDetailsHandler : IRequestHandler<GetPartWithDetailsRequest, ApiResponse<ProjectPartDTO>>
{
	private readonly IProjectPartRepository _projectPartRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

	public GetPartWithDetailsHandler(
        IProjectPartRepository projectPartRepository,
		IBranchRepository branchRepository,
		IMapper mapper)
    {
		_projectPartRepository = projectPartRepository;
        _branchRepository = branchRepository;
        _mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<ProjectPartDTO>> Handle(GetPartWithDetailsRequest request, CancellationToken cancellationToken)
	{
		var projectPart = await _projectPartRepository.GetWithDetailsAsync(request.ProjectPartId, cancellationToken)
			?? throw new PartNotFoundException();
        
		var specification = new BranchesCollectionSpecification(true);

        var availableBranches = await _branchRepository.Get(specification).ToListAsync(cancellationToken: cancellationToken);
		foreach(var item in projectPart.Branches)
		{
			availableBranches.Remove(item.Branch);
		}

		var projectPartDTO = _mapper.Map<ProjectPartDTO>(projectPart);
		projectPartDTO.AvailableBranches = _mapper.Map<ICollection<BranchDTO>>(availableBranches);

        return ApiResponse.Success(projectPartDTO);
	}
}
