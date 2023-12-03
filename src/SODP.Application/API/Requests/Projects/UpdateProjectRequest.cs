using MediatR;
using SODP.Shared.DTO;

namespace SODP.Application.API.Requests.Projects;

public sealed record UpdateProjectRequest() : IRequest
{
	public ProjectDTO Project { get; }
    
	public UpdateProjectRequest(ProjectDTO project) : this()
    {
		Project = project;
	}
}
