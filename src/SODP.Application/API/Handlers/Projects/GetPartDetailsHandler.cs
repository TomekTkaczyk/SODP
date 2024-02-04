using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Mappers;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Attributes;
using SODP.Domain.Exceptions.PartExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetPartDetailsHandler : IRequestHandler<GetPartDetailsRequest, ApiResponse<ProjectPartDTO>>
{
	private readonly IProjectPartRepository _projectPartRepository;
    private readonly IBranchRepository _branchRepository;

	public GetPartDetailsHandler(
        IProjectPartRepository projectPartRepository,
		IBranchRepository branchRepository)
    {
		_projectPartRepository = projectPartRepository;
        _branchRepository = branchRepository;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<ProjectPartDTO>> Handle(GetPartDetailsRequest request, CancellationToken cancellationToken)
	{
		var projectPart = await _projectPartRepository.GetWithDetailsAsync(request.ProjectPartId, cancellationToken)
			?? throw new PartNotFoundException();
        
		var specification = new BranchesCollectionSpecification(true);

        var availableBranches = await _branchRepository
			.Get(specification)
			.ToListAsync(cancellationToken);
		
		foreach(var item in projectPart.Branches)
		{
			availableBranches.Remove(item.Branch);
		}

        return ApiResponse.Success(projectPart.ToDTO(availableBranches));
	}
}
