using SODP.Application.Abstractions;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.Queries.Investors;

public sealed record GetInvestorsPageQuery(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IQuery<Page<InvestorDTO>>
{ }
