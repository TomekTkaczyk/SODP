using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Users;

public sealed record CreateUserRequest(
	string Username,
	string Firstname,
	string Lastname) : IRequest<ApiResponse<UserDTO>>;	
