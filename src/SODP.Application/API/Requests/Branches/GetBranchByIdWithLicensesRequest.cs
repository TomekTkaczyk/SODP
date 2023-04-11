using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Branches;

public sealed record GetBranchByIdWithLicensesRequest(
    int Id) : IRequest<Branch>
{ }
