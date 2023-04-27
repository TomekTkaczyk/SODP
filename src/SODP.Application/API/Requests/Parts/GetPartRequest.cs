using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record GetPartRequest(
	int Id) : IRequest<ApiResponse<ProjectPartDTO>>;
