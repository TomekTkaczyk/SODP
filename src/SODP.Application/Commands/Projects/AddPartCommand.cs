using SODP.Application.Abstractions;
using SODP.Shared.DTO;

namespace SODP.Application.Commands.Projects;

public sealed record AddPartCommand(
	int Id,
	PartDTO part) : ICommand;
