using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Branches;

public sealed record GetBranchRequest(
    int Id) : IRequest<ApiResponse<BranchDTO>>
{ }
