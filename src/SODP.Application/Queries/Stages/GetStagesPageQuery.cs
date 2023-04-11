using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.Response;

namespace SODP.Application.Queries.Stages;

public sealed record GetStagesPageQuery(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IRequest<Page<Stage>> { }
