using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Queries.Projects;

public record GetProjectByIdWithDetailsQuery(
	int Id) : IRequest<Project> { }
