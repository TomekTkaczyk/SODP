using AutoMapper;
using SODP.Application.Abstractions;
using SODP.Domain.Repositories;
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

	public GetProjectsPageQueryHandler(IProjectRepository projectRepository, IMapper mapper)
	{
		_projectRepository = projectRepository;
		_mapper = mapper;
	}

	public async Task<ApiResponse<Page<ProjectDTO>>> Handle(
		GetProjectsPageQuery request, 
		CancellationToken cancellationToken)
	{
		var projectsPage = await _projectRepository.GetPageAsync(
			request.Status,
			request.SearchString,
			request.PageNumber,
			request.PageSize,
			cancellationToken);

		return ApiResponse.Success(
			Page<ProjectDTO>.Create(
				_mapper.Map<IReadOnlyCollection<ProjectDTO>>(projectsPage.Collection),
				projectsPage.PageNumber,
				projectsPage.PageSize,
				projectsPage.TotalCount));
	}
}
