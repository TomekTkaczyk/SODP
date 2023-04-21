using MediatR;

namespace SODP.Application.API.Requests.Parts;

public sealed record DeletePartRequest(
	int Id) : IRequest;
