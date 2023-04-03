using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Projects;

public class GetProjectsPageQueryHandler : IQueryHandler<GetProjectsPageQuery, Page<ProjectDTO>>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IMapper _mapper;

	public GetProjectsPageQueryHandler(
		IProjectRepository projectRepository,
		IMapper mapper)
	{
		_projectRepository = projectRepository;
		_mapper = mapper;
	}

	public async Task<ApiResponse<Page<ProjectDTO>>> Handle(
		GetProjectsPageQuery request, 
		CancellationToken cancellationToken)
	{
		var queryable = _projectRepository.ApplySpecyfication(new ProjectByNameSpecyfication(request.Status, request.SearchString));

		var totalCount = await queryable.CountAsync(cancellationToken);

		if(request.PageSize > 0)
		{
			queryable = _projectRepository.GetPageQuery(queryable, request.PageNumber, request.PageSize);
		}

		var collection = await queryable.ToListAsync(cancellationToken);

		return ApiResponse.Success(
			Page<ProjectDTO>.Create(
				_mapper.Map<IReadOnlyCollection<ProjectDTO>>(collection),
				request.PageNumber,
				request.PageSize,
				totalCount));
	}
}
