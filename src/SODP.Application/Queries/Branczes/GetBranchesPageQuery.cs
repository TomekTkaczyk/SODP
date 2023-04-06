using SODP.Application.Abstractions;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.Queries.Branczes;

public sealed record GetBranchesPageQuery(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IQuery<Page<BranchDTO>>
{ }
