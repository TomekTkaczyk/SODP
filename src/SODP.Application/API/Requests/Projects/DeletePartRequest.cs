using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record DeletePartRequest(int ProjectPartId) : IRequest;
