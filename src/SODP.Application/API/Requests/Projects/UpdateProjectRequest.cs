using MediatR;
using System;

namespace SODP.Application.API.Requests.Projects;

public sealed record UpdateProjectRequest(
	int Id,
	string Name,
	string Title,
	string Address,
	string LocationUnit,
	string BuildingCategory,
	string Investor,
	string BuildingPermit,
	string Description,
	DateTime DevelopmentDate
	) : IRequest;
