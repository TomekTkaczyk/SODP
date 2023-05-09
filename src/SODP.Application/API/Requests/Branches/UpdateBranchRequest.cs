using MediatR;

namespace SODP.Application.API.Requests.Branches;

public sealed record UpdateBranchRequest(
    int Id,
    string Sign,
    string Title) : IRequest;
