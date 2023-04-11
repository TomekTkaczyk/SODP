using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Branches;

public sealed record GetBranchByIdRequest(
    int Id) : IRequest<Branch>
{ }
