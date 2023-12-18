using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Projects;

public sealed record CreateProjectRequest : IRequest<ApiResponse<int>>
{
	public string Number { get; init; }
	public string StageSign { get; init; }
	public string Name { get; init; }

	public CreateProjectRequest(string number, string stageSign, string name)
	{
		Number = number;
		StageSign = stageSign.ToUpper();
		Name = name;
	}
}
