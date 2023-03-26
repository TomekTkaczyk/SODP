using MediatR;
using SODP.Application.Abstractions;
using SODP.Domain.Entities;
using SODP.Shared.Response;

namespace SODP.Application.Commands.Investors
{
	public sealed record CreateInvestorCommand(string Name) : ICommand<Investor>;
}
