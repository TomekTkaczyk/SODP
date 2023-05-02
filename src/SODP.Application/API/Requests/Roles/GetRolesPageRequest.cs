using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Roles;

public sealed record GetRolesPageRequest(
	bool? ActiveStatus,
	int PageNumber,
	int PageSize) : IRequest<ApiResponse<Page<RoleDTO>>>;
