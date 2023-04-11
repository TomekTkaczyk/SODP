using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Queries.Branches;

public sealed record GetBranchByIdWithLicensesQuery(
	int Id) : IRequest<Branch> { }
