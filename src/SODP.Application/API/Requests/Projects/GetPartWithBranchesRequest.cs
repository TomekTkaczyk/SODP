using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Projects;

public sealed record GetPartWithBranchesRequest(int ProjectPartId) : IRequest<ApiResponse<ProjectPartDTO>>;
