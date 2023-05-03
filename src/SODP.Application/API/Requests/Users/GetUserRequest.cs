using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Users;

public sealed record GetUserRequest(
	int Id) : IRequest<ApiResponse<UserDTO>>;
