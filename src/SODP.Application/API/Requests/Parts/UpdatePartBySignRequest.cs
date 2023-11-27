using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record UpdatePartBySignRequest(
	string Sign,
	string Title) : IRequest<ApiResponse>;
