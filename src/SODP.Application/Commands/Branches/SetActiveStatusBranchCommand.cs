using SODP.Application.Abstractions;

namespace SODP.Application.Commands.Branches;

public sealed record SetActiveStatusBranchCommand(
	int Id,
	bool ActiveStatus) : ICommand;

