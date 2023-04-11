using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Common;

public sealed record SetActiveStatusRequest<TEntity>(
    int Id,
    bool ActiveStatus) : IRequest where TEntity : IActiveStatus;
