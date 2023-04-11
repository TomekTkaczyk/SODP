using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Stages;

public sealed record CreateStageRequest(
    string Sign,
    string Name) : IRequest<Stage>;
