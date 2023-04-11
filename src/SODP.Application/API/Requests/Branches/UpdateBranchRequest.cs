using MediatR;
using SODP.Shared.DTO;

namespace SODP.Application.API.Requests.Branches;

public sealed record UpdateBranchRequest(
    int Id,
    BranchDTO Branch) : IRequest;
