using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.Response;

namespace SODP.Application.Queries.Designers;

public sealed record GetDesignersPageQuery(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IRequest<Page<Designer>>
{ }
