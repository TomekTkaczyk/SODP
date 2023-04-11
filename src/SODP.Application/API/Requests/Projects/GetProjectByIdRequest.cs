using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Projects;

public sealed record GetProjectByIdRequest(
	int Id) : IRequest<Project>;
