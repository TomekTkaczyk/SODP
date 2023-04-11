using MediatR;

namespace SODP.Application.API.Requests.Stages;

public sealed record ChangeStageNameRequest(
    int Id,
    string Name) : IRequest;
