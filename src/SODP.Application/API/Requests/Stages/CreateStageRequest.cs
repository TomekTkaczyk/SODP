using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record CreateStageRequest(string Sign, string Title) : IRequest<ApiResponse<int>>;
