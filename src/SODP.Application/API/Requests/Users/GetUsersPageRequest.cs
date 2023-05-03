using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Users;

public sealed record GetUsersPageRequest(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IRequest<ApiResponse<Page<UserDTO>>>;
