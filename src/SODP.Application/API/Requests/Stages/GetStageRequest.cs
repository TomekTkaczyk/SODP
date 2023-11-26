using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record GetStageRequest(int Id) : IRequest<ApiResponse<StageDTO>>;
