using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Branches;

public sealed record GetBranchesPageRequest : IRequest<ApiResponse<Page<BranchDTO>>>
{
	private string _searchString;

	public bool? ActiveStatus { get; init; }
	public string SearchString { get => _searchString; init => _searchString = value?.ToUpper(); }
	public int PageNumber { get; init; } = 1;
	public int PageSize { get; init; } = 0;
}
