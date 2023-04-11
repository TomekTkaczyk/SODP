using MediatR;
using SODP.Application.Abstractions;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Investors;

public sealed record CreateInvestorRequest(
    string Name) : IRequest<Investor>;
