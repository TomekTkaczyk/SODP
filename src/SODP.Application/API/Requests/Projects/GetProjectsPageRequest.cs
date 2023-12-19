using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Projects;

public sealed record GetProjectsPageRequest : IRequest<ApiResponse<Page<ProjectDTO>>>
{
	private string _searchString;

	public ProjectStatus Status { get; init; }
	public string SearchString { get => _searchString; init => _searchString = value?.ToUpper(); }
	public int PageNumber { get; init; } = 1;
	public int PageSize { get; init; } = 0;
}
