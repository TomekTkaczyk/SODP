using MediatR;

namespace SODP.Application.API.Requests.Branches;

public sealed record SetActiveStatusBranchRequest(
    int Id,
    bool ActiveStatus) : IRequest;

