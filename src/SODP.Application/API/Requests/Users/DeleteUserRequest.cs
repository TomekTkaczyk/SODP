using MediatR;

namespace SODP.Application.API.Requests.Users;

public sealed record DeleteUserRequest(int Id) : IRequest;
