using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record GetStagesPageRequest(
	bool? ActiveStatus,
	string SearchString = "",
	int PageNumber = 1,
	int PageSize = 0) : IRequest<ApiResponse<Page<StageDTO>>>;
