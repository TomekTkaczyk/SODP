using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record DeleteProjectRequest(int Id) : IRequest;
