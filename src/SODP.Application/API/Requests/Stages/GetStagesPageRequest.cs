using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record GetStagesPageRequest(
	bool? ActiveStatus,
	string SearchString,
	int PageNumber,
	int PageSize) : IRequest<ApiResponse<Page<StageDTO>>>;
