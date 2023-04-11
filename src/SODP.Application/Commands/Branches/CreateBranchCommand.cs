using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Commands.Branches;

public sealed record CreateBranchCommand(
	string Sign,
	string Name) : IRequest<Branch>;
