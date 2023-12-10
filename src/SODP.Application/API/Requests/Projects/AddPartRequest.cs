using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record AddPartRequest(
	int ProjectId,
	string Sign, 
	string Title) : IRequest;
