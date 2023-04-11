using MediatR;

namespace SODP.Application.Commands.Stages;

public sealed record ChangeStageNameCommand(
	int Id,
	string Name) : IRequest;
