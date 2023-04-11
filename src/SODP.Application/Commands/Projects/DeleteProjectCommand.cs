using MediatR;

namespace SODP.Application.Commands.Projects;

public sealed record DeleteProjectCommand(int Id) : IRequest;
