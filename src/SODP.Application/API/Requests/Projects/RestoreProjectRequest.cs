using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record RestoreProjectRequest(int Id) : IRequest;
