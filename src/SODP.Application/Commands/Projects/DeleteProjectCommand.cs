using SODP.Application.Abstractions;

namespace SODP.Application.Commands.Projects;

public sealed record DeleteProjectCommand(int Id) : ICommand;
