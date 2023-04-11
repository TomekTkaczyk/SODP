using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.Enums;
using SODP.Shared.Response;

namespace SODP.Application.Queries.Projects;

public record GetProjectsPageQuery(
	ProjectStatus Status,
	string SearchString,
	int PageNumber,
	int PageSize) : IRequest<Page<Project>> { }
