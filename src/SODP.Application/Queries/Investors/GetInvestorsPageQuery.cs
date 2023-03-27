using SODP.Application.Abstractions;
using SODP.Application.ValueObjects;
using SODP.Domain.ValueObjects;

namespace SODP.Application.Queries.Investors;

public sealed record GetInvestorsPageQuery(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IQuery<Page<InvestorValueObject>>
{ }
