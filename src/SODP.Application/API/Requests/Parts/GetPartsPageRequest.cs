using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record GetPartsPageRequest(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IRequest<ApiResponse<Page<PartDTO>>>;
