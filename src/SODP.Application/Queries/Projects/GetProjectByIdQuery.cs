using SODP.Application.Abstractions;
using SODP.Shared.DTO;

namespace SODP.Application.Queries.Projects;

public record GetProjectByIdQuery(
	int Id) : IQuery<ProjectDTO> { }
