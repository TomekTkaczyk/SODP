using SODP.Application.Abstractions;
using SODP.Shared.DTO;
using SODP.Shared.Enums;
using SODP.Shared.Response;

namespace SODP.Application.Queries.Projects;

public record GetProjectsPageQuery(
	ProjectStatus Status,
	string SearchString,
	int PageNumber,
	int PageSize) : IQuery<Page<ProjectDTO>> { }
