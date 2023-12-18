using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Projects;

public sealed record GetProjectsPageRequest : IRequest<ApiResponse<Page<ProjectDTO>>>
{					
	public ProjectStatus Status { get; init; }
	public string SearchString { get; init; }
	public int PageNumber { get; init; }
	public int PageSize { get; init; }

	public GetProjectsPageRequest(ProjectStatus status,	string searchString, int pageNumber, int pageSize)
	{	  
		Status = status;
		SearchString = searchString.ToUpper();
		PageNumber = pageNumber;
		PageSize = pageSize;
	}
}
