using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Queries.Projects;

public record GetProjectByIdQuery(
	int Id) : IRequest<Project> { }
