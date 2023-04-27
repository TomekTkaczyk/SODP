using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record UpdatePartRequest(
    int Id,
    string Sign,
    string Title) : IRequest;
