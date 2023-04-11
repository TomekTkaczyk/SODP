using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Commands.Common;

public sealed record SetActiveStatusCommand<TEntity>(
    int Id,
    bool ActiveStatus) : IRequest where TEntity : IActiveStatus;
