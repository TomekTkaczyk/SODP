using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record GetPartsPageRequest : IRequest<ApiResponse<Page<PartDTO>>>
{
	public bool? ActiveStatus { get; init; }
	public string SearchString { get; init; }
	public int PageNumber { get; init; }
	public int PageSize { get; init; }

    public GetPartsPageRequest(bool? activeStatus, string searchString, int pageNumber, int pageSize)
    {																								
		ActiveStatus = activeStatus;
		SearchString = searchString.ToUpper();
		PageNumber = pageNumber;
		PageSize = pageSize;
    }
}	
