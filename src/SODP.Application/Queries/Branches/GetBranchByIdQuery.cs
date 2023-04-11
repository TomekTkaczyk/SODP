using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Queries.Branches;

public sealed record GetBranchByIdQuery(
	int Id) : IRequest<Branch> { }
