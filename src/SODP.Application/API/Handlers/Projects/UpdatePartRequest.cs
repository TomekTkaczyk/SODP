using MediatR;

namespace SODP.Application.API.Handlers.Projects;

public sealed record UpdatePartRequest(
	int Id,
	string Sign,
	string Title) : IRequest;
