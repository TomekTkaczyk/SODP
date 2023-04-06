using SODP.Application.Abstractions;
using SODP.Domain.Entities;

namespace SODP.Application.Commands.Branches;

public sealed record CreateBranchCommand(
	string Sign,
	string Name) : ICommand<Branch>;
