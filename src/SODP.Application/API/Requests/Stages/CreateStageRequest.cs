using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record CreateStageRequest : IRequest<ApiResponse<int>>
{
	public string Sign { get; init; } 
	public string Title { get; init; }

	public CreateStageRequest(string sign, string title = "")
	{
		Sign = sign.ToUpper();
		Title = title.ToUpper();
	}
}
