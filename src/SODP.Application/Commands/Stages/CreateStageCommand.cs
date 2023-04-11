using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Commands.Stages;

public sealed record CreateStageCommand(
	string Sign,
	string Name) : IRequest<Stage>;
