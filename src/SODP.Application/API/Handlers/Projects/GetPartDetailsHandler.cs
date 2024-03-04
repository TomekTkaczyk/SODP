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
using System.Collections.Generic;
using System.Linq;
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
		var projectPart = await _projectPartRepository
			.GetDetailsAsync(request.ProjectPartId, cancellationToken)
			?? throw new PartNotFoundException();
        
		var specification = new BranchesCollectionSpecification(true);

        var availableBranches = await _branchRepository
			.Get(specification)
			.ToListAsync(cancellationToken);
		
		foreach(var item in projectPart.Branches)
		{
			availableBranches.Remove(item.Branch);
		}

        var projectPartDTO = new ProjectPartDTO(
            projectPart.Id,
            projectPart.Sign,
            projectPart.Title,
            projectPart.Order,
            (projectPart.Branches is not null)
                ? projectPart.Branches.Select(y => {
                    return new PartBranchDTO(
                        y.Id,
                        new BranchDTO(y.Branch.Id, y.Branch.Sign, y.Branch.Title, y.Branch.Order, y.Branch.ActiveStatus, new List<LicenseDTO>()),
                        (y.Roles is not null)
                            ? y.Roles.Select(r => {
                                return new BranchRoleDTO(
                                    r.Id,
                                    r.Role.ToString(),
                                    new LicenseDTO(
                                        r.License.Id,
                                        new DesignerDTO(
                                            r.License.Designer.Id,
                                            r.License.Designer.Title,
                                            r.License.Designer.Firstname,
                                            r.License.Designer.Lastname,
                                            r.License.Designer.ActiveStatus,
                                            new List<LicenseDTO>()),
                                        r.License.Content,
                                        new List<BranchDTO>()));
                            })
                            : new List<BranchRoleDTO>());
                })
                : new List<PartBranchDTO>(),
            availableBranches.Select(x => x.ToDTO()));

        return ApiResponse.Success(projectPartDTO);
	}
}
