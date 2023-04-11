using MediatR;
using SODP.Shared.DTO;

namespace SODP.Application.Commands.Branches;

public sealed record UpdateBranchCommand(
	int Id,
	BranchDTO Branch) : IRequest;
