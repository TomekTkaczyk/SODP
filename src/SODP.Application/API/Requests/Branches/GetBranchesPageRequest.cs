using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Branches;

public sealed record GetBranchesPageRequest(
    bool? ActiveStatus,
    string SearchString,
    int PageNumber,
    int PageSize) : IRequest<ApiResponse<Page<BranchDTO>>>
{ }
