using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record RemovePartFromProjectRequest(int ProjectPartId) : IRequest;
