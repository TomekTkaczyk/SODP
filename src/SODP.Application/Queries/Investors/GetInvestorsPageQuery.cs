using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.Response;

namespace SODP.Application.Queries.Investors;

public sealed record GetInvestorsPageQuery(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IRequest<Page<Investor>>
{ }
