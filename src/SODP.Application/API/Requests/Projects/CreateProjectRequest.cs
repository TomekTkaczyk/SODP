using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Projects;

public sealed record CreateProjectRequest : IRequest<ApiResponse<int>>
{
	private string _stageSign;

	public string Number { get; init; }
	public string StageSign { get => _stageSign; init => _stageSign = value?.ToUpper(); }
	public string Name { get; init; }
}
