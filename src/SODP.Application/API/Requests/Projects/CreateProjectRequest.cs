using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Projects;

public sealed record CreateProjectRequest(
	string Number, 
	string StageSign, 
	string Name) : IRequest<ApiResponse<int>>;
