using AutoMapper;
using SODP.Application.Abstractions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Projects;

public class GetProjectByIdWithDetailsQueryHandler : IQueryHandler<GetProjectByIdWithDetailsQuery, ProjectDTO>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IMapper _mapper;

	public GetProjectByIdWithDetailsQueryHandler(
		IProjectRepository projectRepository,
		IMapper mapper)
	{
		_projectRepository = projectRepository;
		_mapper = mapper;
	}

	public async Task<ApiResponse<ProjectDTO>> Handle(
		GetProjectByIdWithDetailsQuery request,
		CancellationToken cancellationToken)
	{
		var project = await _projectRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
		if (project is null)
		{
			var error = new Error(
				"Project.NotFound",
				$"The project with Id:{request.Id} was not found.",
				HttpStatusCode.NotFound);
			return ApiResponse.Failure<ProjectDTO>(error, HttpStatusCode.NotFound);
		}

		return ApiResponse.Success(_mapper.Map<ProjectDTO>(project), HttpStatusCode.OK);
	}
}
