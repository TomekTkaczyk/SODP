using MediatR;
using SODP.Shared.DTO;

namespace SODP.Application.API.Requests.Projects;

public sealed record UpdateProjectRequest(
    int Id,
    ProjectDTO Project) : IRequest;
