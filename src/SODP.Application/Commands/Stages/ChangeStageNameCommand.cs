using SODP.Application.Abstractions;
using SODP.Domain.Entities;

namespace SODP.Application.Commands.Stages;

public sealed record ChangeStageNameCommand(
	int Id,
	string Name) : ICommand;
