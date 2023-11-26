using MediatR;

namespace SODP.Application.API.Requests.Stages;

public sealed record DeleteStageRequest(int Id) : IRequest;

