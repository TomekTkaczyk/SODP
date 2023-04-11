using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.Enums;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Projects;

public record GetProjectsPageRequest(
    ProjectStatus Status,
    string SearchString,
    int PageNumber,
    int PageSize) : IRequest<Page<Project>>;
