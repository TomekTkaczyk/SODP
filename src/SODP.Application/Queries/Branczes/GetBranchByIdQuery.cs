using SODP.Application.Abstractions;
using SODP.Shared.DTO;

namespace SODP.Application.Queries.Branczes;

public sealed record GetBranchByIdQuery(int Id) : IQuery<BranchDTO>
{
}
