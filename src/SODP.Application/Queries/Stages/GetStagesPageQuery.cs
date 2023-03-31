using SODP.Application.Abstractions;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.Queries.Stages;

public sealed record GetStagesPageQuery(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IQuery<Page<StageDTO>>
{ }
