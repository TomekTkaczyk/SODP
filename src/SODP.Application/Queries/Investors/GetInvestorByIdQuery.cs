using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Queries.Investors;

public sealed record GetInvestorByIdQuery(int Id) : IRequest<Investor> { }
