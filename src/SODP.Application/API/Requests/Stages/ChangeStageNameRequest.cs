using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record ChangeStageNameRequest(
    int Id,
    string Name) : IRequest<ApiResponse>;
