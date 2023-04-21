using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record CreatePartRequest(
	string Sign,
	string Title) : IRequest<ApiResponse<PartDTO>>;
