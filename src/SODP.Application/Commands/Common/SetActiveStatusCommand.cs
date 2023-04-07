using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.Response;

namespace SODP.Application.Commands.Common;

public sealed record SetActiveStatusCommand<TEntity>(
    int Id,
    bool ActiveStatus) : IRequest<ApiResponse> where TEntity : IActiveStatus;
