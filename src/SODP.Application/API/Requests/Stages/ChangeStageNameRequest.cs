using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record ChangeStageTitleRequest(
    int Id,
    string Title) : IRequest<ApiResponse>;
