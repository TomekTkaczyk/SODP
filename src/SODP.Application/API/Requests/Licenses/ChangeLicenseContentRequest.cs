using MediatR;

namespace SODP.Application.API.Requests.Licenses;

public sealed record ChangeLicenseContentRequest(
	int Id,
	string Content) : IRequest;
