using MediatR;

namespace SODP.Application.Commands.Branches;

public sealed record SetActiveStatusBranchCommand(
	int Id,
	bool ActiveStatus) : IRequest;

