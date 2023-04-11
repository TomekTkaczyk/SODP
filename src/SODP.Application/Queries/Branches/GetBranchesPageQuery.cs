using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.Application.Queries.Branches;

public sealed record GetBranchesPageQuery(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IRequest<Page<Branch>>
{ }
