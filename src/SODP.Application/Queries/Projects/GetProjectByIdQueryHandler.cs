using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Projects;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project>
{
	private readonly IProjectRepository _projectRepository;

	public GetProjectByIdQueryHandler(
		IProjectRepository projectRepository)
    {
		_projectRepository = projectRepository;
	}

    public async Task<Project> Handle(
		GetProjectByIdQuery request, 
		CancellationToken cancellationToken)
	{
		var project = await _projectRepository
			.ApplySpecyfication(new ProjectByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		return project ?? throw new NotFoundException("Project");
	}
}
