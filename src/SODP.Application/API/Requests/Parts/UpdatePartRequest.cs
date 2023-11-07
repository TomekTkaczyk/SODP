using MediatR;

namespace SODP.Application.API.Requests.Parts;

public sealed record UpdatePartRequest(
	int Id, 
	string Sign,
	string Title) : IRequest;
