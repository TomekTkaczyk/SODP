using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Projects;

public sealed record AddPartRequest(
	int Id, 
	string Sign, 
	string Title) : IRequest<ApiResponse<PartDTO>>;
