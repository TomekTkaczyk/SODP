using MediatR;
using SODP.Application.Abstractions;
using SODP.Domain.Entities;

namespace SODP.Application.Commands.Investors;

public sealed record CreateInvestorCommand(
	string Name) : IRequest<Investor>;
