using MediatR;
using SODP.Shared.DTO;

namespace SODP.Application.Commands.Projects;

public sealed record UpdateProjectCommand(
	int Id,
	ProjectDTO project) : IRequest;
