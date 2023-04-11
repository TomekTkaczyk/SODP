using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.Response;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Projects;

public class GetProjectsPageQueryHandler : IRequestHandler<GetProjectsPageQuery, Page<Project>>
{
	private readonly IProjectRepository _projectRepository;

	public GetProjectsPageQueryHandler(
		IProjectRepository projectRepository)
	{
		_projectRepository = projectRepository;
	}

	public async Task<Page<Project>> Handle(
		GetProjectsPageQuery request, 
		CancellationToken cancellationToken)
	{
		var queryable = _projectRepository.ApplySpecyfication(new ProjectByNameSpecyfication(request.Status, request.SearchString));

		var totalCount = await queryable.CountAsync(cancellationToken);

		if(request.PageSize > 0)
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
