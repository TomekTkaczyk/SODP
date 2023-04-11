using MediatR;
using SODP.Application.Abstractions;

namespace SODP.Application.Commands.Investors
{
	public sealed record ChangeInvestorNameCommand(int Id, string Name) : IRequest;
}
