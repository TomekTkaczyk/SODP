using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record UpdatePartByIdRequest(
	int Id, 
	string Title) : IRequest<ApiResponse>;
