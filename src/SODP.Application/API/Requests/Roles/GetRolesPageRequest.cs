using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Roles;

public sealed record GetRolesPageRequest : IRequest<ApiResponse<Page<RoleDTO>>>
{
	private string _searchString;

	public bool? ActiveStatus { get; init; }
	public string SearchString { get => _searchString; init => _searchString = value?.ToUpper(); }
	public int PageNumber { get; init; } = 1;
	public int PageSize { get; init; } = 0;
}
