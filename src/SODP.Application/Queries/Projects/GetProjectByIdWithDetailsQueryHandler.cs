using MediatR;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Projects;

public class GetProjectByIdWithDetailsQueryHandler : IRequestHandler<GetProjectByIdWithDetailsQuery, Project>
{
	private readonly IProjectRepository _projectRepository;

	public GetProjectByIdWithDetailsQueryHandler(
		IProjectRepository projectRepository)
	{
		_projectRepository = projectRepository;
	}

	public async Task<Project> Handle(
		GetProjectByIdWithDetailsQuery request,
		CancellationToken cancellationToken)
	{
		var project = await _projectRepository
			.GetByIdWithDetailsAsync(request.Id, cancellationToken);
		
		return project ?? throw new NotFoundException("Project");
	}
}
