using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record GetStagesPageRequest() : IRequest<ApiResponse<Page<StageDTO>>>
{
	public bool? ActiveStatus { get; init; }
	public string SearchString { get; init; }
	public int PageNumber { get; init; }
	public int PageSize { get; init; }

    public GetStagesPageRequest(bool? activeStatus, string searchString, int? pageNumber, int? pageSize) : this()
    {
		ActiveStatus = activeStatus;
		SearchString = searchString.ToUpper();
		PageNumber = pageNumber ?? 1;
		PageSize = pageSize ?? 0;
    }
}
