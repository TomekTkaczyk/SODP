using MediatR;

namespace SODP.Application.API.Requests.Users;

public sealed record UpdateUserRequest(
	int Id,
	string Username,
	string Firstname,
	string Lastname) : IRequest;