using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record UpdateStageByIdRequest(
	int Id,
	string Title) : IRequest<ApiResponse>;
