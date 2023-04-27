using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Projects;

public sealed record GetPartRequest(int ProjectPartId) : IRequest<ApiResponse<ProjectPartDTO>>;
