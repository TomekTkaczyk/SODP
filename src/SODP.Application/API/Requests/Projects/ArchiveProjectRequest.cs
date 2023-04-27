using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record ArchiveProjectRequest(int Id) : IRequest;
