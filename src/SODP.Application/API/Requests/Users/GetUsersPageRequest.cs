using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Users;

public sealed record GetUsersPageRequest : IRequest<ApiResponse<Page<UserDTO>>>
{
	public bool? ActiveStatus { get; init; }
	public string SearchString { get; init; }
	public int PageNumber { get; init; }
	public int PageSize { get; init; }

	public GetUsersPageRequest(bool? activeStatus, string searchString, int pageNumber, int pageSize)
	{
		ActiveStatus = activeStatus;
		SearchString = searchString.ToUpper();
		PageNumber = pageNumber;
		PageSize = pageSize;
	}
}
