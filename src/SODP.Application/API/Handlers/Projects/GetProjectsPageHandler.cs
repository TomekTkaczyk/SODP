using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.Response;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public class GetProjectsPageHandler : IRequestHandler<GetProjectsPageRequest, Page<Project>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectsPageHandler(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Page<Project>> Handle(
        GetProjectsPageRequest request,
        CancellationToken cancellationToken)
    {
        var queryable = _projectRepository.ApplySpecyfication(new ProjectByNameSpecyfication(request.Status, request.SearchString));

        var totalCount = await queryable.CountAsync(cancellationToken);

        if (request.PageSize > 0)
        {
            queryable = _projectRepository.GetPageQuery(queryable, request.PageNumber, request.PageSize);
        }

        var collection = new ReadOnlyCollection<Project>(await queryable.ToListAsync(cancellationToken));

        return Page<Project>.Create(
                collection,
                request.PageNumber,
                request.PageSize,
                totalCount);
    }
}
