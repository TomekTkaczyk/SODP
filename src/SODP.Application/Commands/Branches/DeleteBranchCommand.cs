using SODP.Application.Abstractions;

namespace SODP.Application.Commands.Branches
{
	public sealed record DeleteBranchCommand(int Id) : ICommand;
}
