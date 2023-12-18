using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Branches;

public sealed record GetBranchesPageRequest : IRequest<ApiResponse<Page<BranchDTO>>>
{
	public bool? ActiveStatus { get; init; }
	public string SearchString { get; init; }
	public int PageNumber { get; init; }
	public int PageSize { get; init; }

    public GetBranchesPageRequest(bool? activeStatus, string searchString, int pageNumber, int pageSize)
    {                                                                                                  
        ActiveStatus = activeStatus;
        SearchString = searchString.ToUpper();
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
