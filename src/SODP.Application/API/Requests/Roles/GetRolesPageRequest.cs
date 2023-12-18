using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Roles;

public sealed record GetRolesPageRequest : IRequest<ApiResponse<Page<RoleDTO>>>
{
	public bool? ActiveStatus { get; init; }
	public string SearchString { get; init; }
	public int PageNumber { get; init; }
	public int PageSize { get; init; }

	public GetRolesPageRequest(bool? activeStatus, string searchString, int pageNumber, int pageSize)
	{
		ActiveStatus = activeStatus;
		SearchString = searchString.ToUpper();
		PageNumber = pageNumber;
		PageSize = pageSize;
	}
}
