using MediatR;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Mappers;
using SODP.Domain.Attributes;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetProjectDetailsHandler : IRequestHandler<GetProjectDetailsRequest, ApiResponse<ProjectDTO>>
{
    private readonly IProjectRepository _projectRepository;

	public GetProjectDetailsHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<ProjectDTO>> Handle(
        GetProjectDetailsRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetDetailsAsync(request.Id, cancellationToken)
			?? throw new ProjectNotFoundException();

        var projectDTO = new ProjectDTO
        {
            Id = project.Id,
            Number = (project.Number is not null) ? project.Number : "",
            Stage = project.Stage.ToDTO(),
            Name = (project.Name is not null) ? project.Name : "",
            Title = (project.Title is not null) ? project.Title : "",
            Address = (project.Address is not null) ? project.Address : "",
            LocationUnit = project.LocationUnit,
            BuildingCategory = project.BuildingCategory,
            BuildingPermit = project.BuildingPermit,
            Investor = project.Investor,
            Description = project.Description,
            DevelopmentDate = project.DevelopmentDate,
            Status = project.Status,
            Parts = (project.Parts is not null)
                ? project.Parts.Select(x => {
                    return new ProjectPartDTO(
                        x.Id,
                        x.Sign,
                        x.Title,
                        x.Order,
                        (x.Branches is not null)
                            ? x.Branches.Select(y => {
                                return new PartBranchDTO(
                                    y.Id,
                                    new BranchDTO(y.Branch.Id,y.Branch.Sign,y.Branch.Title,y.Branch.Order,y.Branch.ActiveStatus,new List<LicenseDTO>()),
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
                        new List<BranchDTO>());
                    })
                : new List<ProjectPartDTO>()
        };

        return ApiResponse.Success(projectDTO);
    }
}
