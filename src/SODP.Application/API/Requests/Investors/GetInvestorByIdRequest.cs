using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Investors;

public sealed record GetInvestorByIdRequest(
	int Id) : IRequest<Investor>;
